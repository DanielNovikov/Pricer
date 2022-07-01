using System;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Dialog.Services.Concrete;

public class UserItemParser : IUserItemParser
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly IParser _parser;
    private readonly IResourceService _resourceService;
    private readonly IItemService _itemService;
    
    public UserItemParser(
        IUserActionLogger userActionLogger,
        IParser parser, 
        IResourceService resourceService, 
        IItemService itemService)
    {
        _userActionLogger = userActionLogger;
        _parser = parser;
        _resourceService = resourceService;
        _itemService = itemService;
    }

    public async Task<UserItemParseServiceResult> Parse(User user, Uri url, ShopKey shop)
    {
        var parseResult = await _parser.Parse(url, shop);

        if (!parseResult.IsSuccess)
        {
            _userActionLogger.LogParsingError(user, url, parseResult.Error);
            return UserItemParseServiceResult.Fail(parseResult.Error);
        }

        var parsedItem = parseResult.Result;

        var item = await _itemService.Create(
            parsedItem.Price,
            parsedItem.Title,
            url,
            parsedItem.ImageUrl,
            user.Id,
            shop);
        
        _userActionLogger.LogItemAdded(user, item);

        var successMessage = _resourceService.Get(ResourceKey.Dialog_ItemAdded);
        return UserItemParseServiceResult.Success(successMessage);
    }
}