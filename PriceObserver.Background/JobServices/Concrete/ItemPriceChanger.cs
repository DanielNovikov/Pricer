using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Background.JobServices.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Telegram.Abstract;

namespace PriceObserver.Background.JobServices.Concrete;

public class ItemPriceChanger : IItemPriceChanger
{
    private readonly IResourceService _resourceService;
    private readonly IItemService _itemService;
    private readonly ILogger _logger;
    private readonly ITelegramBotService _telegramBotService;
    private readonly IItemParseResultService _parseResultService;
    private readonly IShopRepository _shopRepository;
    
    public ItemPriceChanger(
        IResourceService resourceService, 
        IItemService itemService, 
        ILogger<ItemPriceChanger> logger, 
        ITelegramBotService telegramBotService, 
        IItemParseResultService parseResultService, 
        IShopRepository shopRepository)
    {
        _resourceService = resourceService;
        _itemService = itemService;
        _logger = logger;
        _telegramBotService = telegramBotService;
        _parseResultService = parseResultService;
        _shopRepository = shopRepository;
    }

    public async Task Change(Item item, int oldPrice, int newPrice)
    {
        await _parseResultService.CreateSucceeded(item);
        
        if (newPrice == oldPrice)
            return;

        var shop = _shopRepository.GetByKey(item.ShopKey);
        var currencyTitle = _resourceService.Get(shop.Currency.Title);
        
        var priceMessage = newPrice < oldPrice
            ? _resourceService.Get(ResourceKey.Background_ItemPriceWentDown, item.Url, newPrice, currencyTitle)
            : _resourceService.Get(ResourceKey.Background_ItemPriceGrewUp, item.Url, newPrice, currencyTitle);

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
}