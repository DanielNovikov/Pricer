using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Menus.Concrete.Handlers;

public class WriteToSupportMenuInputHandler : IMenuInputHandler
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly IResourceService _resourceService;

    public WriteToSupportMenuInputHandler(
        IUserActionLogger userActionLogger,
        IResourceService resourceService)
    {
        _userActionLogger = userActionLogger;
        _resourceService = resourceService;
    }

    public MenuKey Key => MenuKey.Support;
        
    public Task<MenuInputHandlingServiceResult> Handle(MessageModel message)
    {
        _userActionLogger.LogWriteToSupport(message.User, message.Text);

        var responseMessage = _resourceService.Get(ResourceKey.Dialog_SupportReply);
        var reply = ReplyResult.Reply(responseMessage);
        var result = MenuInputHandlingServiceResult.Success(reply); 
            
        return Task.FromResult(result);
    }
}