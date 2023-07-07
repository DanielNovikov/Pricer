using System;
using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;
using Pricer.Parser.Abstract;

namespace Pricer.Dialog.Services.Concrete;

public class UserItemParser : IUserItemParser
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly IParser _parser;
    private readonly IItemService _itemService;
    private readonly IShopRepository _shopRepository;
    private readonly IItemRepository _itemRepository;
    
    public UserItemParser(
        IUserActionLogger userActionLogger,
        IParser parser,
        IItemService itemService,
        IShopRepository shopRepository, 
        IItemRepository itemRepository)
    {
        _userActionLogger = userActionLogger;
        _parser = parser;
        _itemService = itemService;
        _shopRepository = shopRepository;
        _itemRepository = itemRepository;
    }

    public async ValueTask<IReplyResult> Parse(User user, Uri url)
    {   
        var item = await _itemRepository.GetByUserIdAndUrl(user.Id, url);
        if (item is not null)
        {
            if (!item.IsDeleted)
            {
                _userActionLogger.LogDuplicateItem(user, url);
                return new ReplyResourceResult(ResourceKey.Dialog_DuplicateItem);
            }

            await _itemService.Restore(item);
            return new ReplyResourceResult(ResourceKey.Dialog_ItemAdded);
        }
        
        var shop = _shopRepository.GetByHost(url.Host);
        if (shop is null)
        {
            _userActionLogger.LogTriedToAddUnsupportedShop(user, url);
            return new ReplyResourceResult(ResourceKey.Dialog_ShopIsNotAvailable);
        }
        
        var parseResult = await _parser.Parse(url, shop.Key);
        if (!parseResult.IsSuccess)
        {
            _userActionLogger.LogParsingError(user, url, parseResult.Error);
            return new ReplyResourceResult(parseResult.Error);
        }

        var parsedItem = parseResult.Result;

        item = await _itemService.Create(
            parsedItem.Price, parsedItem.Title, url, parsedItem.ImageUrl,
            user.Id, shop.Key, parsedItem.IsAvailable, parsedItem.CurrencyKey);
        
        if (item.IsAvailable)
            _userActionLogger.LogItemAdded(user, item);
        else 
            _userActionLogger.LogNotAvailableItemAdded(user, item);
        
        return new ReplyResourceResult(ResourceKey.Dialog_ItemAdded);
    }
}