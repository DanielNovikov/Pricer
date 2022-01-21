using System;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Dialog.Services.Concrete;

public class UserItemParser : IUserItemParser
{
    private readonly IShopRepository _shopRepository;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IParser _parser;
    private readonly IResourceService _resourceService;
    private readonly IItemService _itemService;
    private readonly IItemRepository _itemRepository;
    
    public UserItemParser(
        IShopRepository shopRepository,
        IUserActionLogger userActionLogger,
        IParser parser, 
        IResourceService resourceService, 
        IItemService itemService, 
        IItemRepository itemRepository)
    {
        _shopRepository = shopRepository;
        _userActionLogger = userActionLogger;
        _parser = parser;
        _resourceService = resourceService;
        _itemService = itemService;
        _itemRepository = itemRepository;
    }

    public async Task<UserItemParseServiceResult> Parse(User user, Uri url)
    {
        var itemExists = await _itemRepository.ExistsForUserByUrl(user.Id, url);
        if (itemExists)
        {
            _userActionLogger.LogDuplicateItem(user, url);
            return UserItemParseServiceResult.Fail(ResourceKey.Dialog_DuplicateItem);
        }
        
        var shop = _shopRepository.GetByHost(url.Host);

        if (shop == null)
        {
            _userActionLogger.LogTriedToAddUnsupportedShop(user, url);
            return UserItemParseServiceResult.Fail(ResourceKey.Dialog_ShopIsNotAvailable);
        }

        var parseResult = await _parser.Parse(url, shop.Key);

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
            shop.Key);
        
        _userActionLogger.LogItemAdded(user, item);

        var successMessage = _resourceService.Get(ResourceKey.Dialog_ItemAdded);
        return UserItemParseServiceResult.Success(successMessage);
    }
}