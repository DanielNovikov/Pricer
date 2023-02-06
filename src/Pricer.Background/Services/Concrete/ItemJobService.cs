using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pricer.Background.Services.Abstract;
using Pricer.Common.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Parser.Models;

namespace Pricer.Background.Services.Concrete;

public class ItemJobService : IItemJobService
{
    private readonly IItemService _itemService;
    private readonly IResourceService _resourceService;
    private readonly IUserRepository _userRepository;
    private readonly IUserLanguage _userLanguage;
    private readonly IPartnerUrlBuilder _partnerUrlBuilder;
    private readonly IBotService _botService;
    private readonly IItemRepository _itemRepository;
    private readonly IItemParseResultService _parseResultService;
    private readonly ICurrencyService _currencyService;
    private readonly ILogger<ItemJobService> _logger;
    private readonly IItemPriceChangedKeyboardBuilder _itemPriceChangedKeyboardBuilder;

    public ItemJobService(
        IItemService itemService,
        IResourceService resourceService,
        IUserRepository userRepository,
        IUserLanguage userLanguage,
        IPartnerUrlBuilder partnerUrlBuilder, 
        IBotService botService, 
        IItemRepository itemRepository, 
        IItemParseResultService parseResultService, 
        ICurrencyService currencyService, 
        ILogger<ItemJobService> logger, 
        IItemPriceChangedKeyboardBuilder itemPriceChangedKeyboardBuilder)
    {
        _itemService = itemService;
        _resourceService = resourceService;
        _userRepository = userRepository;
        _userLanguage = userLanguage;
        _partnerUrlBuilder = partnerUrlBuilder;
        _botService = botService;
        _itemRepository = itemRepository;
        _parseResultService = parseResultService;
        _currencyService = currencyService;
        _logger = logger;
        _itemPriceChangedKeyboardBuilder = itemPriceChangedKeyboardBuilder;
    }

    public async ValueTask UpdateIsAvailable(Item item, bool isAvailable)
    {
        if (item.IsAvailable == isAvailable)
            return;

        await _itemService.UpdateIsAvailable(item, isAvailable);

        var user = await _userRepository.GetById(item.UserId);
        _userLanguage.Set(user.SelectedLanguageKey);
		
        var partnerUrl = _partnerUrlBuilder.Build(item.Url);
        var resourceTemplate = item.IsAvailable
            ? ResourceKey.Background_ItemIsInStock
            : ResourceKey.Background_ItemIsOutOfStock;
            
        var availabilityMessage = _resourceService.Get(resourceTemplate, partnerUrl, item.Title);
        await _botService.SendText(user.BotKey, user.ExternalId, availabilityMessage);
    }

    public async Task Update(Item item, ParsedItem parsedItem)
    {
        item.Title = parsedItem.Title;
        item.ImageUrl = parsedItem.ImageUrl;
        await _itemRepository.Update(item);
        
        var newPrice = parsedItem.Price;
        await UpdatePrice(item, newPrice);
    }
    
    public async Task Remove(Item item, ResourceKey error)
    {
        const int countOfFailedToRemove = 10;
    
        var lastErrorsCount = await _parseResultService.GetLastErrorsCount(item.Id);
        if (lastErrorsCount < countOfFailedToRemove)
        {
            _logger.LogInformation("Item with url {0} is about to be removed. Errors count: {1}", item.Url, lastErrorsCount);
            await _parseResultService.CreateFailed(item);
            return;
        }

        var user = await _userRepository.GetById(item.UserId);
        _userLanguage.Set(user.SelectedLanguageKey);
        
        var errorReason = _resourceService.Get(error);
        var partnerUrl = _partnerUrlBuilder.Build(item.Url);
        
        var itemDeletedMessage = _resourceService.Get(
            ResourceKey.Background_ItemDeleted,
            partnerUrl, item.Title, errorReason);

        await _botService.SendText(user.BotKey, user.ExternalId, itemDeletedMessage);
        await _itemService.Delete(item);
        
        _logger.LogInformation(
            "Item {0} deleted\nReason: {1}\nLink: {2}", 
            item.Title, errorReason, item.Url);
    }
    
    private async Task UpdatePrice(Item item, int newPrice)
    {
        await _parseResultService.CreateSucceeded(item);
        
        var oldPrice = item.Price;
        if (newPrice == oldPrice)
            return;

        var user = await _userRepository.GetById(item.UserId);
        _userLanguage.Set(user.SelectedLanguageKey);
        
        var priceChangedAboveThreshold = HasPriceChangedAboveThreshold(oldPrice, newPrice, user.MinimumDiscountThreshold);
        var notifyUser = priceChangedAboveThreshold && (newPrice < oldPrice || user.GrowthPriceNotificationsEnabled);

        if (notifyUser || newPrice > oldPrice)
        {
            await _itemService.UpdatePrice(item, newPrice);
            LogChangedPrice(item, oldPrice, newPrice);
        }
        
        if (notifyUser)
        {
            var difference = Math.Abs(oldPrice - newPrice);

            var currencyTitle = _currencyService.GetTitle(item.CurrencyKey);

            var resourceTemplate = newPrice < oldPrice
                ? ResourceKey.Background_ItemPriceWentDown
                : ResourceKey.Background_ItemPriceGrewUp;
            
            var priceChangedMessage = _resourceService.Get(
                resourceTemplate, item.Title, newPrice, currencyTitle, difference, currencyTitle);

            var keyboard = _itemPriceChangedKeyboardBuilder.Build(item);
            
            await _botService.SendTextWithMessageKeyboard(user.BotKey, user.ExternalId, priceChangedMessage, keyboard);
        }
    }

    private static bool HasPriceChangedAboveThreshold(int oldPrice, int newPrice, int threshold)
    {
        const double oneHundredPercent = 100.0;
        
        var difference = Math.Abs(oldPrice - newPrice);
        var differenceRatio = difference * oneHundredPercent / oldPrice;

        return differenceRatio > threshold;
    }

    private void LogChangedPrice(Item item, int oldPrice, int newPrice)
    {
        var logMessage = _resourceService.Get(
            ResourceKey.Background_LogItemPriceChanged,
            oldPrice, newPrice, item.Url);
        
        _logger.LogInformation(logMessage);
    }
}