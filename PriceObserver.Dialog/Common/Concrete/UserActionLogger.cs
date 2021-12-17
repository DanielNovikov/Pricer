﻿using System;
using Microsoft.Extensions.Logging;
using PriceObserver.Common.Extensions;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Common.Abstract;

namespace PriceObserver.Dialog.Common.Concrete
{
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

        public void LogWrongUrlPassed(User user, string messageText, ResourceKey error)
        {
            var errorMessage = _resourceService.Get(error);
            LogError(user, ResourceKey.UserAction_PassedWrongUrl, messageText, errorMessage);
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
            var message = _resourceService.Get(resource, parameters);
            var userInfo = GetUserInfo(user);

            _logger.Log(logLevel, $"{message}{Environment.NewLine}{userInfo}");
        }

        private string GetUserInfo(User user)
        {
            var info = _resourceService.Get(ResourceKey.UserAction_UserInfo, user.GetFullName(), user.Id);

            if (!string.IsNullOrEmpty(user.Username))
            {
                info += Environment.NewLine;
                info += _resourceService.Get(ResourceKey.UserAction_UserLogin, user.Username);
            }

            return info;
        }
    }
}