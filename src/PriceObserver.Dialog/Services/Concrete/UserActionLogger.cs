﻿using System;
using Microsoft.Extensions.Logging;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Extensions;
using PriceObserver.Dialog.Services.Abstract;

namespace PriceObserver.Dialog.Services.Concrete;

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
        var info = _resourceService.Get(ResourceKey.UserAction_UserInfo, user.GetFullName(), user.ExternalId);

        if (!string.IsNullOrEmpty(user.Username))
        {
            info += Environment.NewLine;
            info += _resourceService.Get(ResourceKey.UserAction_UserLogin, user.Username);
        }

        return info;
    }
}