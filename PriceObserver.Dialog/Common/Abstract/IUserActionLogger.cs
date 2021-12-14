using System;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Dialog.Common.Abstract
{
    public interface IUserActionLogger
    {
        void LogAllItemsCalled(User user);

        void LogShopsCalled(User user);
        
        void LogWebsiteCalled(User user);
        
        void LogUserRegistered(User user);
        
        void LogWrongUrlPassed(User user, string messageText, ResourceKey error);
        
        void LogDuplicateItem(User user, Uri url);
        
        void LogParsingError(User user, Uri url, ResourceKey error);

        void LogItemAdded(User user, Item item);
        
        void LogWriteToSupport(User user, string messageText);
        
        void LogWrongCommand(User user, string messageText);

        void LogRedirectToMenu(User user, Menu menuToRedirect);

        void LogHelpCalled(User user);
    }
}