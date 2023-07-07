using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Menus.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Menus.Concrete.Handlers;

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
    
    public async ValueTask<IReplyResult> Handle(MessageModel message)
    {
        var user = message.User;
        
        var urlExtractionResult = _urlExtractor.Extract(message.Text);
        if (!urlExtractionResult.IsSuccess)
            return _wrongCommandHandler.Handle(message);

        var url = urlExtractionResult.Result;
        
        return await _userItemParser.Parse(user, url);
    }
}