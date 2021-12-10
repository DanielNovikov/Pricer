using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Parser.Abstract;
using PriceObserver.Telegram.Abstract;

namespace PriceObserver.Background.Jobs
{
    public class ItemsObserverJob : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        
        public ItemsObserverJob(IServiceProvider serviceProvider)
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
                    var itemService = scope.ServiceProvider.GetService<IItemService>();
                    var resourceService = scope.ServiceProvider.GetService<IResourceService>();
                    
                    var items = await itemRepository!.GetAll();

                    foreach (var item in items)
                    {
                        var parsedItemResult = await parserService!.Parse(item.Url);

                        if (!parsedItemResult.IsSuccess)
                        {
                            await itemRepository.Delete(item);

                            var errorReason = resourceService!.Get(parsedItemResult.Error);
                            var itemDeletedMessage = resourceService.Get(
                                ResourceKey.Background_ItemDeleted,
                                item.Url.ToString(),
                                item.Title,
                                errorReason);
                            
                            await telegramBotService!.SendMessage(item.UserId, itemDeletedMessage);
                            
                            continue;
                        }

                        var parsedItem = parsedItemResult.Result;

                        var oldPrice = item.Price;
                        var newPrice = parsedItem.Price;
                        
                        if (newPrice == oldPrice)
                            continue;

                        var priceMessage = newPrice < oldPrice
                            ? resourceService!.Get(ResourceKey.Background_ItemPriceWentDown, item.Url, newPrice)
                            : resourceService!.Get(ResourceKey.Background_ItemPriceGrewUp, item.Url, newPrice);

                        var message = resourceService.Get(
                            ResourceKey.Background_ItemPriceChanged, 
                            item.Title,
                            priceMessage);
                        
                        await telegramBotService!.SendMessage(item.UserId, message);

                        await itemService!.UpdatePrice(item, newPrice);
                    }

                    await Task.Delay(TimeSpan.FromMinutes(30), cancellationToken);
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}