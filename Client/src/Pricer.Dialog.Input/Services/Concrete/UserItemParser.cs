using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Input.Models;
using Pricer.Dialog.Input.Services.Abstract;
using Pricer.Parser.Abstract;

namespace Pricer.Dialog.Input.Services.Concrete;

public class UserItemParser : IUserItemParser
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly IParser _parser;
    private readonly IItemService _itemService;
    private readonly IShopRepository _shopRepository;
    
    public UserItemParser(
        IUserActionLogger userActionLogger,
        IParser parser,  
        IItemService itemService,
        IShopRepository shopRepository)
    {
        _userActionLogger = userActionLogger;
        _parser = parser;
        _itemService = itemService;
        _shopRepository = shopRepository;
    }

    public async ValueTask<UserItemParseResult> Parse(User user, Uri url)
    {   
        var shop = _shopRepository.GetByHost(url.Host);
        if (shop is null)
        {
            _userActionLogger.LogTriedToAddUnsupportedShop(user, url);
            return UserItemParseResult.Fail(ResourceKey.Dialog_ShopIsNotAvailable);
        }
        
        var parseResult = await _parser.Parse(url, shop.Key);

        if (!parseResult.IsSuccess)
        {
            _userActionLogger.LogParsingError(user, url, parseResult.Error);
            return UserItemParseResult.Fail(parseResult.Error);
        }

        var parsedItem = parseResult.Result;

        var item = await _itemService.Create(
            parsedItem.Price, parsedItem.Title, url, parsedItem.ImageUrl,
            user.Id, shop.Key, parsedItem.IsAvailable, parsedItem.CurrencyKey);
        
        if (item.IsAvailable)
            _userActionLogger.LogItemAdded(user, item);
        else 
            _userActionLogger.LogNotAvailableItemAdded(user, item);
        
        return UserItemParseResult.Success(ResourceKey.Dialog_ItemAdded);
    }
}