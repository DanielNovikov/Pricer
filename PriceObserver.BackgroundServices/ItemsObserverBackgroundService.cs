using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Parser.Abstract;
using PriceObserver.Telegram.Abstract.Client;

namespace PriceObserver.Jobs
{
    public class ItemsObserverBackgroundService : IHostedService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IParserService _parserService;
        private readonly ITelegramBotService _telegramBotService;
        
        private CancellationTokenSource _tokenSource;
        
        public ItemsObserverBackgroundService(
            IItemRepository itemRepository,
            IParserService parserService, 
            ITelegramBotService telegramBotService)
        {
            _itemRepository = itemRepository;
            _parserService = parserService;
            _telegramBotService = telegramBotService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _tokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (!_tokenSource.IsCancellationRequested)
                {
                    var items = await _itemRepository.GetAll();

                    foreach (var item in items)
                    {
                        try
                        {
                            var parsedItem = await _parserService.Parse(item.Url);

                            if (parsedItem.Price != item.Price)
                            {
                                await _telegramBotService.SendMessage(
                                    item.UserId,
                                    $"Price changed from {item.Price} to {parsedItem.Price}\n{item.Url}");

                                item.Price = parsedItem.Price;
                                await _itemRepository.Update(item);
                            }
                        }
                        catch (Exception ex)
                        {
                            await _telegramBotService.SendMessage(
                                item.UserId,
                                $"Cannot parse item {item.Url} \r\n Reason: '{ex.Message}'");
                        }
                    }

                    await Task.Delay(TimeSpan.FromMinutes(30), cancellationToken);
                }
            });
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _tokenSource?.Cancel();
        }
    }
}