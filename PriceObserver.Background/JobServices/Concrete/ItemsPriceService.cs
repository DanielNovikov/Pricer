using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
    private readonly ITelegramBotService _telegramBotService;
    private readonly IItemService _itemService;
    private readonly IResourceService _resourceService;
    private readonly ILogger _logger;
    
    public ItemsPriceService(
        IItemRepository itemRepository,
        IParserService parserService,
        ITelegramBotService telegramBotService,
        IItemService itemService,
        IResourceService resourceService,
        ILogger<ItemsPriceService> logger)
    {
        _itemRepository = itemRepository;
        _parserService = parserService;
        _telegramBotService = telegramBotService;
        _itemService = itemService;
        _resourceService = resourceService;
        _logger = logger;
    }

    public async Task Observe()
    {
        var items = await _itemRepository.GetAll();

        foreach (var item in items)
        {
            var parsedItemResult = await _parserService.Parse(item.Url);

            if (!parsedItemResult.IsSuccess)
            {
                await DeleteItem(item, parsedItemResult);
                return;
            }

            await ObservePrice(item, parsedItemResult);
        }
    }

    private async Task ObservePrice(Item item, ParsedItemResult parsedItemResult)
    {
        var parsedItem = parsedItemResult.Result;

        var oldPrice = item.Price;
        var newPrice = parsedItem.Price;

        if (newPrice == oldPrice)
            return;

        var priceMessage = newPrice < oldPrice
            ? _resourceService.Get(ResourceKey.Background_ItemPriceWentDown, item.Url, newPrice)
            : _resourceService.Get(ResourceKey.Background_ItemPriceGrewUp, item.Url, newPrice);

        LogChangedPrice(item, oldPrice, newPrice);
        await SendChangedPrice(item, priceMessage);
        await _itemService.UpdatePrice(item, newPrice);
    }

    private void LogChangedPrice(Item item, int oldPrice, int newPrice)
    {
        var logMessage = _resourceService.Get(
            ResourceKey.Background_LogItemPriceChanged,
            oldPrice,
            newPrice,
            item.Url);
        
        _logger.LogInformation(logMessage);
    }

    private async Task SendChangedPrice(Item item, string priceMessage)
    {
        var message = _resourceService.Get(ResourceKey.Background_ItemPriceChanged, item.Title, priceMessage);
        await _telegramBotService.SendMessage(item.UserId, message);
    }

    private async Task DeleteItem(Item item, ParsedItemResult parsedItemResult)
    {
        await _itemRepository.Delete(item);

        var errorReason = _resourceService.Get(parsedItemResult.Error);
        var itemDeletedMessage = _resourceService.Get(
            ResourceKey.Background_ItemDeleted,
            item.Url.ToString(),
            item.Title,
            errorReason);

        await _telegramBotService.SendMessage(item.UserId, itemDeletedMessage);
    }
}