using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Parser.Abstract;
using PriceObserver.Telegram.Client.Abstract;

namespace PriceObserver.Jobs
{
    public class ItemsObserverBackgroundService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        
        public ItemsObserverBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    using var scope = _serviceProvider.CreateScope();

                    var itemRepository = scope.ServiceProvider.GetService<IItemRepository>();
                    var parserService = scope.ServiceProvider.GetService<IParserService>();
                    var telegramBotService = scope.ServiceProvider.GetService<ITelegramBotService>();
                    
                    var items = await itemRepository.GetAll();

                    foreach (var item in items)
                    {
                        var parsedItemResult = await parserService.Parse(item.Url);

                        if (!parsedItemResult.IsSuccess)
                        {
                            await telegramBotService.SendMessage(
                                item.UserId,
                                $"Не можем получить данные о <a href='{item.Url}'>товаре</a>.\r\nПричина: '{parsedItemResult.Error}'.");
                            
                            continue;
                        }

                        var parsedItem = parsedItemResult.Result;

                        var oldPrice = item.Price;
                        var newPrice = parsedItem.Price;
                        
                        if (newPrice == oldPrice)
                            continue;

                        var message = newPrice < oldPrice
                            ? $"📉 Цена на <a href='{item.Url}'>товар</a> уменьшилась с <b>{oldPrice}</b> до <b>{newPrice}</b>"
                            : $"📈 Цена на <a href='{item.Url}'>товар</a> увеличилась с <b>{oldPrice}</b> до <b>{newPrice}</b>";
                        
                        await telegramBotService.SendMessage(item.UserId, message);

                        item.Price = newPrice;
                        await itemRepository.Update(item);
                    }

                    await Task.Delay(TimeSpan.FromMinutes(30), cancellationToken);
                }
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}