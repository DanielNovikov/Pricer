using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Menus.Concrete.Handlers;

public class HomeMenuInputHandler : IMenuInputHandler
{
    private readonly IUrlExtractor _urlExtractor;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IUserItemParser _userItemParser;
    private readonly IItemRepository _itemRepository;
    private readonly IWrongCommandHandler _wrongCommandHandler;
    private readonly IItemService _itemService;

    public HomeMenuInputHandler(
        IUrlExtractor urlExtractor,
        IUserActionLogger userActionLogger,
        IUserItemParser userItemParser, 
        IItemRepository itemRepository,
        IWrongCommandHandler wrongCommandHandler,
        IItemService itemService)
    {
        _urlExtractor = urlExtractor;
        _userActionLogger = userActionLogger;
        _userItemParser = userItemParser;
        _itemRepository = itemRepository;
        _wrongCommandHandler = wrongCommandHandler;
        _itemService = itemService;
    }

    public MenuKey Key => MenuKey.Home;
    
    public async ValueTask<MenuInputHandlingServiceResult> Handle(MessageModel message)
    {
        var user = message.User;
        
        var urlExtractionResult = _urlExtractor.Extract(message.Text);
        if (!urlExtractionResult.IsSuccess)
        {
            var result = _wrongCommandHandler.Handle(message);
            return MenuInputHandlingServiceResult.Success(result);
        }

        var url = urlExtractionResult.Result;
        var item = await _itemRepository.GetByUserIdAndUrl(user.Id, url);
        if (item is not null)
        {
            if (!item.IsDeleted)
            {
                _userActionLogger.LogDuplicateItem(user, url);
                return MenuInputHandlingServiceResult.Fail(ResourceKey.Dialog_DuplicateItem);
            }

            await _itemService.Restore(item);
            
            var result = new ReplyResourceResult(ResourceKey.Dialog_ItemAdded);
            return MenuInputHandlingServiceResult.Success(result);
        }
        
        var parseResult = await _userItemParser.Parse(user, url);

        if (!parseResult.IsSuccess)
            return MenuInputHandlingServiceResult.Fail(parseResult.Error);

        var replyResult = new ReplyResourceResult(parseResult.Result);
        return MenuInputHandlingServiceResult.Success(replyResult);
    }
}