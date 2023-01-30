using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pricer.Background.Services.Abstract;
using Pricer.Bot.Abstract;
using Pricer.Common.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Telegram.Abstract;

namespace Pricer.Background.Services.Concrete;

public class ItemPriceService : IItemPriceService
{
    private readonly IResourceService _resourceService;
    private readonly IItemService _itemService;
    private readonly ILogger _logger;
    private readonly IItemParseResultService _parseResultService;
    private readonly IUserRepository _userRepository;
    private readonly IUserLanguage _userLanguage;
    private readonly ICurrencyService _currencyService;
    private readonly IItemDeletionKeyboardBuilder _itemDeletionKeyboardBuilder;
    private readonly IBotService _botService;

    private const double OneHundredPercent = 100.0;
    
    public ItemPriceService(
        IResourceService resourceService, 
        IItemService itemService, 
        ILogger<ItemPriceService> logger,
        IItemParseResultService parseResultService, 
        IUserRepository userRepository, 
        IUserLanguage userLanguage,
        ICurrencyService currencyService,
        IItemDeletionKeyboardBuilder itemDeletionKeyboardBuilder,
        IBotService botService)
    {
        _resourceService = resourceService;
        _itemService = itemService;
        _logger = logger;
        _parseResultService = parseResultService;
        _userRepository = userRepository;
        _userLanguage = userLanguage;
        _currencyService = currencyService;
        _itemDeletionKeyboardBuilder = itemDeletionKeyboardBuilder;
        _botService = botService;
    }

    public async Task Change(Item item, int newPrice)
    {
        await _parseResultService.CreateSucceeded(item);
        
        var oldPrice = item.Price;
        if (newPrice == oldPrice)
            return;

        var user = await _userRepository.GetById(item.UserId);
        _userLanguage.Set(user.SelectedLanguageKey);
        
        var priceChangedAboveThreshold = 
            HasPriceChangedAboveThreshold(oldPrice, newPrice, user.MinimumDiscountThreshold);

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

            var keyboard = _itemDeletionKeyboardBuilder.Build(item);
            
            await _botService.SendTextWithMessageKeyboard(user.BotKey, user.ExternalId, priceChangedMessage, keyboard);
        }
    }

    private static bool HasPriceChangedAboveThreshold(int oldPrice, int newPrice, int threshold)
    {
        var difference = Math.Abs(oldPrice - newPrice);
        var differenceRatio = difference * OneHundredPercent / oldPrice;

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