using System;
using Microsoft.Extensions.Logging;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Services.Concrete;

public class UserActionLogger : IUserActionLogger
{
    private readonly ILogger<UserActionLogger> _logger;
    private readonly IResourceService _resourceService;

    public UserActionLogger(
        ILogger<UserActionLogger> logger,
        IResourceService resourceService)
    {
        _logger = logger;
        _resourceService = resourceService;
    }

    public void LogAllItemsCalled(User user)
    {
        LogInformation(user, ResourceKey.UserAction_GotAddedProducts);
    }

    public void LogShopsCalled(User user)
    {
        LogInformation(user, ResourceKey.UserAction_GotAvailableShops);
    }

    public void LogWebsiteCalled(User user)
    {
        LogInformation(user, ResourceKey.UserAction_GotWebsiteLink);
    }

    public void LogUserRegistered(User user)
    {
        LogInformation(user, ResourceKey.UserAction_UserRegistered);
    }

    public void LogDuplicateItem(User user, Uri url)
    {
        LogError(user, ResourceKey.UserAction_TriedAddDuplicate, url);
    }

    public void LogParsingError(User user, Uri url, ResourceKey error)
    {
        var errorMessage = _resourceService.Get(error);
        LogError(user, ResourceKey.UserAction_ParsingError, url, errorMessage);
    }

    public void LogItemAdded(User user, Item item)
    {
        LogInformation(user, ResourceKey.UserAction_AddedItem, item.Url, item.Title, item.Price);
    }

    public void LogWriteToSupport(User user, string messageText)
    {
        LogInformation(user, ResourceKey.UserAction_WroteToSupport, messageText);
    }

    public void LogWrongCommand(User user, string messageText)
    {
        LogInformation(user, ResourceKey.UserAction_WroteWrongCommand, messageText);
    }

    public void LogRedirectToMenu(User user, Menu menuToRedirect)
    {
        LogInformation(user, ResourceKey.UserAction_RedirectedToMenu, menuToRedirect.Key);
    }

    public void LogRedirectBackToMenu(User user, Menu menuToRedirectBack)
    {
        LogInformation(user, ResourceKey.UserAction_RedirectedBackToMenu, menuToRedirectBack.Key);
    }

    public void LogHelpCalled(User user)
    {
        LogInformation(user, ResourceKey.UserAction_CalledHelp);
    }

    public void LogTriedToAddUnsupportedShop(User user, Uri url)
    {
        LogInformation(user, ResourceKey.UserAction_TriedToAddUnsupportedShop, url);
    }

    public void LogGotAddItemInstruction(User user)
    {
        LogInformation(user, ResourceKey.UserAction_GotAddItemInstruction);
    }

    public void LogSelectedLanguage(User user, LanguageKey languageKey)
    {
        LogInformation(user, ResourceKey.UserAction_SelectedLanguage, languageKey.ToString());
    }

    public void LogToggledPriceGrowthNotifications(User user, bool enabled)
    {
        LogInformation(user, ResourceKey.UserAction_ToggledPriceGrowthNotifications, enabled);
    }

    public void LogNotAvailableItemAdded(User user, Item item)
    {
        LogInformation(user, ResourceKey.UserAction_AddedNotAvailableItem, item.Url, item.Title, item.Price);
    }
    
    public void LogDeletedItem(User user, Item item)
    {
        LogInformation(user, ResourceKey.UserAction_DeletedItem, item.Url, item.Title, item.Id);
    }

    public void LogRestoredItem(User user, Item item)
    {
        LogInformation(user, ResourceKey.UserAction_RestoredItem, item.Url, item.Title, item.Id);
    }

    public void LogCalledTogglingPriceGrowingMenu(User user)
    {
        LogInformation(user, ResourceKey.UserAction_CalledTogglingPriceGrowingMenu);
    }

    public void LogCalledChangingLanguageMenu(User user)
    {
        LogInformation(user, ResourceKey.UserAction_CalledChangingLanguageMenu);
    }

    public void LogCalledChangingMinimumDiscountThreshold(User user)
    {
        LogInformation(user, ResourceKey.UserAction_CalledChangingMinimumDiscountThreshold);
    }

    public void LogChangedMinimumDiscountThreshold(User user, int discountThreshold)
    {
        LogInformation(user, ResourceKey.UserAction_ChangedMinimumDiscountThreshold, discountThreshold);
    }

    private void LogInformation(User user, ResourceKey message, params object[] parameters)
    {
        Log(user, LogLevel.Information, message, parameters);
    }

    private void LogError(User user, ResourceKey message, params object[] parameters)
    {
        Log(user, LogLevel.Error, message, parameters);
    }

    private void Log(User user, LogLevel logLevel, ResourceKey resource, params object[] parameters)
    {
        if (!_logger.IsEnabled(logLevel))
            return;
        
        var message = _resourceService.Get(resource, parameters);
        var userInfo = GetUserInfo(user);

        var logMessage = string.Concat(message, Environment.NewLine, userInfo);
        _logger.Log(logLevel, logMessage);
    }

    private string GetUserInfo(User user)
    {
        var info = _resourceService.Get(ResourceKey.UserAction_UserInfo, user.FullName, user.ExternalId);

        if (!string.IsNullOrEmpty(user.Username))
        {
            info += Environment.NewLine;
            info += _resourceService.Get(ResourceKey.UserAction_UserLogin, user.Username);
        }

        return info;
    }
}