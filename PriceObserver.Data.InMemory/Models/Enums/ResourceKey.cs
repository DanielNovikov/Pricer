namespace PriceObserver.Data.InMemory.Models.Enums;

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
        
    Parser_PageNotFound = 200,
    Parser_NoItemInfoOnPage = 202,
    Parser_OutOfStock = 203,
        
    Background_ItemDeleted = 300,
    Background_ItemPriceWentDown = 301,
    Background_LogItemPriceChanged = 302,
        
    Menu_Home = 400,
    Menu_Support = 401,
        
    Command_Back = 500,
    Command_Add = 501,
    Command_AllItems = 502,
    Command_Shops = 503,
    Command_Website = 504,
    Command_WriteToSupport = 505,
    Command_Help = 506,
    
    Currency_UAH_Title = 600,
    Currency_EUR_Title = 601,
    Currency_UAH_Sign = 602,
    Currency_EUR_Sign = 603,
    
    Api_NoHistory = 700,
    Api_GrewUpSign = 701,
    Api_WentDownSign = 702,
    Api_UrlTemplate = 703
}