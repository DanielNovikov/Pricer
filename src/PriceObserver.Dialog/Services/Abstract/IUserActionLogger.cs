using System;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserActionLogger
{
    void LogAllItemsCalled(User user);

    void LogShopsCalled(User user);
        
    void LogWebsiteCalled(User user);
        
    void LogUserRegistered(User user);

    void LogDuplicateItem(User user, Uri url);
        
    void LogParsingError(User user, Uri url, ResourceKey error);

    void LogItemAdded(User user, Item item);
        
    void LogWriteToSupport(User user, string messageText);
        
    void LogWrongCommand(User user, string messageText);

    void LogRedirectToMenu(User user, Menu menuToRedirect);

    void LogHelpCalled(User user);

    void LogTriedToAddUnsupportedShop(User user, Uri url);

    void LogGotAddItemInstruction(User user);
}