using System;
using System.Resources;
using System.Threading.Tasks;
using PriceObserver.Background.JobServices.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;
using PriceObserver.Telegram.Abstract;

namespace PriceObserver.Background.JobServices.Concrete;

public class ItemsPriceService : IItemsPriceService
{
    private readonly IItemRepository _itemRepository;
    private readonly IParserService _parserService;
    private readonly IItemPriceChanger _itemPriceChanger;
    private readonly IItemParseResultRepository _parseResultRepository;
    private readonly IItemParseResultService _parseResultService;
    private readonly IItemRemovalService _itemRemovalService;

    public ItemsPriceService(
        IItemRepository itemRepository,
        IParserService parserService,
        IItemPriceChanger itemPriceChanger,
        IItemParseResultRepository parseResultRepository,
        IItemParseResultService parseResultService,
        IItemRemovalService itemRemovalService)
    {
        _itemRepository = itemRepository;
        _parserService = parserService;
        _itemPriceChanger = itemPriceChanger;
        _parseResultRepository = parseResultRepository;
        _parseResultService = parseResultService;
        _itemRemovalService = itemRemovalService;
    }

    public async Task Observe()
    {
        var items = await _itemRepository.GetAll();

        foreach (var item in items)
        {
            var parsedItemResult = await _parserService.Parse(item.Url);

            if (!parsedItemResult.IsSuccess)
            {
                await DeleteItem(item, parsedItemResult.Error);
                return;
            }

            await ObservePrice(item, parsedItemResult.Result);
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