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

    void LogRedirectBackToMenu(User user, Menu menuToRedirectBack);

    void LogHelpCalled(User user);

    void LogTriedToAddUnsupportedShop(User user, Uri url);

    void LogGotAddItemInstruction(User user);

    void LogSelectedLanguage(User user, LanguageKey languageKey);

    void LogToggledPriceGrowthNotifications(User user, bool enabled);
    
    void LogNotAvailableItemAdded(User user, Item item);
    
    void LogDeletedItem(User user, Item item);

    void LogRestoredItem(User user, Item item);

    void LogCalledTogglingPriceGrowingMenu(User user);

    void LogCalledChangingLanguageMenu(User user);

    void LogCalledChangingMinimumDiscountThreshold(User user);
    
    void LogChangedMinimumDiscountThreshold(User user, int discountThreshold);
}