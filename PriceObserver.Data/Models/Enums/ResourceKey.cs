namespace PriceObserver.Data.Models.Enums
{
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

        UserAction_GotAddedProducts = 100,
        UserAction_ParsingError = 101,
        UserAction_GotWebsiteLink = 102,
        UserAction_UserRegistered = 103,
        UserAction_PassedWrongUrl = 104,
        UserAction_TriedAddDuplicate = 105,
        UserAction_AddedItem = 106,
        UserAction_WroteToSupport = 107,
        UserAction_WroteWrongCommand = 108,
        UserAction_RedirectedToMenu = 109,
        UserAction_UserInfo = 110,
        UserAction_UserLogin = 111,
        UserAction_GotAvailableShops = 112,
        
        Parser_PageNotFound = 200,
        Parser_ShopIsNotAvailable = 201,
        Parser_NoPriceOnPage = 202,
        
        Background_ItemDeleted = 300,
        Background_ItemPriceWentDown = 301,
        Background_ItemPriceGrewUp = 302,
        Background_ItemPriceChanged = 303,
        
        Menu_Home = 400,
        Menu_NewItem = 401,
        Menu_Support = 402,
        
        Command_Back = 500,
        Command_Add = 501,
        Command_AllItems = 502,
        Command_Shops = 503,
        Command_Website = 504,
        Command_WriteToSupport = 505
    }
}