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
    private readonly IResourceService _resourceService;

    public HomeMenuInputHandler(
        IUrlExtractor urlExtractor,
        IUserActionLogger userActionLogger,
        IUserItemParser userItemParser, 
        IItemRepository itemRepository,
        IWrongCommandHandler wrongCommandHandler,
        IItemService itemService,
        IResourceService resourceService)
    {
        _urlExtractor = urlExtractor;
        _userActionLogger = userActionLogger;
        _userItemParser = userItemParser;
        _itemRepository = itemRepository;
        _wrongCommandHandler = wrongCommandHandler;
        _itemService = itemService;
        _resourceService = resourceService;
    }

    public MenuKey Key => MenuKey.Home;
    
    public async Task<MenuInputHandlingServiceResult> Handle(MessageModel message)
    {
        var user = message.User;
        
        var urlExtractionResult = _urlExtractor.Extract(message.Text);
        if (!urlExtractionResult.IsSuccess)
        {
            var replyResult = _wrongCommandHandler.Handle(message);
            return MenuInputHandlingServiceResult.Success(replyResult);
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
            var replyMessage = _resourceService.Get(ResourceKey.Dialog_ItemAdded);
            var replyResult = ReplyResult.Reply(replyMessage);
            return MenuInputHandlingServiceResult.Success(replyResult);
        }
        
        var parseResult = await _userItemParser.Parse(user, url);

        if (!parseResult.IsSuccess)
            return MenuInputHandlingServiceResult.Fail(parseResult.Error);

        var reply = ReplyResult.Reply(parseResult.Result);
        return MenuInputHandlingServiceResult.Success(reply);
    }
}