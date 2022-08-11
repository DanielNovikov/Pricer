using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Background.Services.Abstract;
using PriceObserver.Common.Services.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Telegram.Abstract;

namespace PriceObserver.Background.Services.Concrete;

public class ItemPriceChanger : IItemPriceChanger
{
    private readonly IResourceService _resourceService;
    private readonly IItemService _itemService;
    private readonly ILogger _logger;
    private readonly ITelegramBotService _telegramBotService;
    private readonly IItemParseResultService _parseResultService;
    private readonly IPartnerUrlBuilder _partnerUrlBuilder;
    private readonly IUserRepository _userRepository;
    private readonly IUserLanguage _userLanguage;
    private readonly ICurrencyService _currencyService;

    private const double OneHundredPercent = 100.0;
    private const int MinimumDifferenceRatio = 5;
    
    public ItemPriceChanger(
        IResourceService resourceService, 
        IItemService itemService, 
        ILogger<ItemPriceChanger> logger, 
        ITelegramBotService telegramBotService, 
        IItemParseResultService parseResultService, 
        IPartnerUrlBuilder partnerUrlBuilder, 
        IUserRepository userRepository, 
        IUserLanguage userLanguage,
        ICurrencyService currencyService)
    {
        _resourceService = resourceService;
        _itemService = itemService;
        _logger = logger;
        _telegramBotService = telegramBotService;
        _parseResultService = parseResultService;
        _partnerUrlBuilder = partnerUrlBuilder;
        _userRepository = userRepository;
        _userLanguage = userLanguage;
        _currencyService = currencyService;
    }

    public async Task Change(Item item, int newPrice)
    {
        await _parseResultService.CreateSucceeded(item);
        
        var oldPrice = item.Price;
        if (newPrice == oldPrice)
            return;

        var user = await _userRepository.GetById(item.UserId);
        _userLanguage.Set(user.SelectedLanguageKey);
        
        var priceDecreased = HasPriceDecreased(oldPrice, newPrice);

        if (priceDecreased ||
            (newPrice > oldPrice && user.GrowthPriceNotificationsEnabled))
        {
            var difference = Math.Abs(oldPrice - newPrice);
            var partnerUrl = _partnerUrlBuilder.Build(item.Url);

            var currencyTitle = _currencyService.GetTitle(item.CurrencyKey);

            var resourceTemplate = priceDecreased
                ? ResourceKey.Background_ItemPriceWentDown
                : ResourceKey.Background_ItemPriceGrewUp;
            
            var priceChangedMessage = _resourceService.Get(
                resourceTemplate, item.Title, partnerUrl, newPrice, currencyTitle, difference, currencyTitle);
            
            await _telegramBotService.SendMessage(user.ExternalId, priceChangedMessage);
        }

        LogChangedPrice(item, oldPrice, newPrice);
        await _itemService.UpdatePrice(item, newPrice);
    }

    private static bool HasPriceDecreased(int oldPrice, int newPrice)
    {
        if (newPrice > oldPrice)
            return false;

        var difference = oldPrice - newPrice;
        var differenceRatio = difference * OneHundredPercent / oldPrice;

        return differenceRatio > MinimumDifferenceRatio;
    }

    private void LogChangedPrice(Item item, int oldPrice, int newPrice)
    {
        var logMessage = _resourceService.Get(
            ResourceKey.Background_LogItemPriceChanged,
            oldPrice, newPrice, item.Url);
        
        _logger.LogInformation(logMessage);
    }
}