using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Background.Jobs;
using PriceObserver.Background.Services.Abstract;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Background.Services.Concrete;

public class ItemsObserverService : IItemsObserverService
{
    private readonly IItemRepository _itemRepository;
    private readonly IParser _parser;
    private readonly IItemPriceChanger _itemPriceChanger;
    private readonly IItemRemovalService _itemRemovalService;
    private readonly ILogger _logger;

    public ItemsObserverService(
        IItemRepository itemRepository,
        IParser parser,
        IItemPriceChanger itemPriceChanger,
        IItemRemovalService itemRemovalService,
        ILogger<ItemsObserverService> logger)
    {
        _itemRepository = itemRepository;
        _parser = parser;
        _itemPriceChanger = itemPriceChanger;
        _itemRemovalService = itemRemovalService;
        _logger = logger;
    }

    public async Task Observe()
    {
        try
        {
            var items = await _itemRepository.GetAll();

            foreach (var item in items)
            {
                try
                {
                    var parsedItemResult = await _parser.Parse(item.Url, item.ShopKey);

                    if (!parsedItemResult.IsSuccess)
                    {
                        await _itemRemovalService.Remove(item, parsedItemResult.Error);
                        break;
                    }

                    var oldPrice = item.Price;
                    var newPrice = parsedItemResult.Result.Price;
                    await _itemPriceChanger.Change(item, oldPrice, newPrice);
                }
                catch (Exception ex)
                {
                    const string templateLogMessage = @"Background parser threw an exception
Item: {0} (Id: {1})
Item url: {2}
Message: {3}
InnerException: {4}";

                    _logger.LogError(
                        ex,
                        templateLogMessage,
                        item.Title,
                        item.Id,
                        item.Url,
                        ex.Message,
                        ex.InnerException);
                }
                finally
                {
                    await Task.Delay(1000);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Unexpected error occured in {0} with exception of type {1}",
                nameof(ItemsObserver),
                ex.GetType().FullName);
        }
    }
}