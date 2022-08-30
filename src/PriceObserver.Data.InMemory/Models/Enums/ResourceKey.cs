﻿namespace PriceObserver.Data.InMemory.Models.Enums;

public enum ResourceKey
{
    Dialog_UserRegistered = 0,
    Dialog_IncorrectCommand = 1,
    Dialog_Website = 2,
    Dialog_EmptyCart = 3,
    Dialog_ItemInfo = 4,
    Dialog_AvailableShops = 5,
    Dialog_DuplicateItem = 6,
    Dialog_ItemAdded = 7,
    Dialog_SupportReply = 8,
    Dialog_MessageDoesNotContainLink = 9,
    Dialog_LinkInIncorrectFormat = 10,
    Dialog_ErrorOccured = 11,
    Dialog_Help = 12,
    Dialog_ShopIsNotAvailable = 13,
    Dialog_AddItemInformation = 14,
    Dialog_MaximumOfItemsExceeded = 15,
    Dialog_RestoreItem = 16,
    Dialog_GoByItemUrl = 17,
    Dialog_ItemDeleted = 18,
    Dialog_ChangeLanguageToRussian = 19,
    Dialog_ChangeLanguageToUkrainian = 20,
    Dialog_ChangeLanguage = 21,
    Dialog_LanguageChanged = 22,
    
    UserAction_GotAddedProducts = 100,
    UserAction_ParsingError = 101,
    UserAction_GotWebsiteLink = 102,
    UserAction_UserRegistered = 103,
    UserAction_TriedAddDuplicate = 105,
    UserAction_AddedItem = 106,
    UserAction_WroteToSupport = 107,
    UserAction_WroteWrongCommand = 108,
    UserAction_RedirectedToMenu = 109,
    UserAction_UserInfo = 110,
    UserAction_UserLogin = 111,
    UserAction_GotAvailableShops = 112,
    UserAction_CalledHelp = 113,
    UserAction_TriedToAddUnsupportedShop = 114,
    UserAction_GotAddItemInstruction = 115,
    UserAction_SelectedLanguage = 116,
    UserAction_RedirectedBackToMenu = 117,
    UserAction_ToggledPriceGrowthNotifications = 118,
    UserAction_AddedNotAvailableItem = 119,
    UserAction_DeletedItem = 120,
    UserAction_RestoredItem = 121,
        
    Parser_PageNotFound = 200,
    Parser_NoItemInfoOnPage = 202,
        
    Background_ItemDeleted = 300,
    Background_ItemPriceWentDown = 301,
    Background_LogItemPriceChanged = 302,
    Background_ItemPriceGrewUp = 303,
    Background_ItemIsInStock = 304,
    Background_ItemIsOutOfStock = 305,
    Background_DeleteItem = 306,
    Background_GoByItemUrl = 307,
        
    Menu_Home = 400,
    Menu_Support = 401,
    Menu_SelectLanguage = 402,
    Menu_Settings = 403,
    Menu_TogglePriceGrowing = 404,
        
    Command_Back = 500,
    Command_Add = 501,
    Command_AllItems = 502,
    Command_Shops = 503,
    Command_Website = 504,
    Command_WriteToSupport = 505,
    Command_Help = 506,
    Command_Settings = 507,
    Command_SelectLanguage = 508,
    Command_TogglePriceGrowing = 509,
    Command_SelectUkrainianLanguage = 510,
    Command_SelectRussianLanguage = 511,
    Command_EnablePriceGrowing = 512,
    Command_DisablePriceGrowing = 513,
    
    Currency_UAH_Title = 600,
    Currency_UAH_Sign = 601,
    Currency_EUR_Title = 602,
    Currency_EUR_Sign = 603,
    Currency_USD_Title = 604,
    Currency_USD_Sign = 605,
    
    Api_NoHistory = 700,
    Api_GrewUpSign = 701,
    Api_WentDownSign = 702,
    Api_UrlTemplate = 703,
    
    ShopCategory_Clothes = 800,
    ShopCategory_Electronics = 801,
    ShopCategory_Food = 802,
    ShopCategory_Cosmetics = 803,
    ShopCategory_MarketPlaces = 804,
    ShopCategory_Different = 805,
    
    AppNotification_HowToAddItem = 900
}