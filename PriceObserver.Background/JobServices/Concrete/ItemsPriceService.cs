using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Background.JobServices.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Background.JobServices.Concrete;

public class ItemsPriceService : IItemsPriceService
{
    private readonly IItemRepository _itemRepository;
    private readonly IParser _parser;
    private readonly IItemPriceChanger _itemPriceChanger;
    private readonly IItemParseResultRepository _parseResultRepository;
    private readonly IItemParseResultService _parseResultService;
    private readonly IItemRemovalService _itemRemovalService;
    private readonly ILogger _logger;

    public ItemsPriceService(
        IItemRepository itemRepository,
        IParser parser,
        IItemPriceChanger itemPriceChanger,
        IItemParseResultRepository parseResultRepository,
        IItemParseResultService parseResultService,
        IItemRemovalService itemRemovalService,
        ILogger<ItemsPriceService> logger)
    {
        _itemRepository = itemRepository;
        _parser = parser;
        _itemPriceChanger = itemPriceChanger;
        _parseResultRepository = parseResultRepository;
        _parseResultService = parseResultService;
        _itemRemovalService = itemRemovalService;
        _logger = logger;
    }

    public async Task Observe()
    {
        var items = await _itemRepository.GetAll();

        foreach (var item in items)
        {
            try
            {
                var parsedItemResult = await _parser.Parse(item.Url, item.ShopKey);

                if (!parsedItemResult.IsSuccess)
                {
                    await DeleteItem(item, parsedItemResult.Error);
                    return;
                }

                await ObservePrice(item, parsedItemResult.Result);
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
        }
    }

    private async Task ObservePrice(Item item, ParsedItem parsedItem)
    {
        var oldPrice = item.Price;
        var newPrice = parsedItem.Price;

        await _itemPriceChanger.Change(item, oldPrice, newPrice);
    }

    private async Task DeleteItem(Item item, ResourceKey error)
    {
        var lastParseResult = await _parseResultRepository.GetLastByItemId(item.Id);

        if (lastParseResult is null || lastParseResult.IsSuccess)
        {
            await _parseResultService.CreateFailed(item);
            return;
        }

        await _itemRemovalService.Remove(item, error);
    }
}