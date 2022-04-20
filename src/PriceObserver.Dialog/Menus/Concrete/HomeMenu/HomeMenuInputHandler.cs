using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Menus.Models;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Menus.Concrete.HomeMenu;

public class HomeMenuInputHandler : IMenuInputHandler
{
    private readonly IUrlExtractor _urlExtractor;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IUserItemParser _userItemParser;
    private readonly IItemRepository _itemRepository;
    private readonly IShopRepository _shopRepository;
    private readonly IWrongCommandHandler _wrongCommandHandler;

    public HomeMenuInputHandler(
        IUrlExtractor urlExtractor,
        IUserActionLogger userActionLogger,
        IUserItemParser userItemParser, 
        IItemRepository itemRepository,
        IShopRepository shopRepository, 
        IWrongCommandHandler wrongCommandHandler)
    {
        _urlExtractor = urlExtractor;
        _userActionLogger = userActionLogger;
        _userItemParser = userItemParser;
        _itemRepository = itemRepository;
        _shopRepository = shopRepository;
        _wrongCommandHandler = wrongCommandHandler;
    }

    public MenuKey Key => MenuKey.Home;
    
    public async Task<MenuInputHandlingServiceResult> Handle(MessageServiceModel message)
    {
        var user = message.User;
        
        var urlExtractionResult = _urlExtractor.Extract(message.Text);
        if (!urlExtractionResult.IsSuccess)
        {
            var replyResult = _wrongCommandHandler.Handle(message);
            return MenuInputHandlingServiceResult.Success(replyResult);
        }

        var url = urlExtractionResult.Result;
        var itemExists = await _itemRepository.ExistsByUserIdAndUrl(user.Id, url);
        if (itemExists)
        {
            _userActionLogger.LogDuplicateItem(user, url);
            return MenuInputHandlingServiceResult.Fail(ResourceKey.Dialog_DuplicateItem);
        }
        
        var shop = _shopRepository.GetByHost(url.Host);
        if (shop is null)
        {
            _userActionLogger.LogTriedToAddUnsupportedShop(user, url);
            return MenuInputHandlingServiceResult.Fail(ResourceKey.Dialog_ShopIsNotAvailable);
        }
        
        var parseResult = await _userItemParser.Parse(user, url, shop.Key);

        if (!parseResult.IsSuccess)
            return MenuInputHandlingServiceResult.Fail(parseResult.Error);

        var reply = ReplyResult.Reply(parseResult.Result);
        return MenuInputHandlingServiceResult.Success(reply);
    }
}