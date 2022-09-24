using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Common.Models.Callback;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Input.Menus.Abstract;
using Pricer.Dialog.Input.Models;

namespace Pricer.Dialog.Input.Menus.Concrete.Handlers;

public class WriteToSupportMenuInputHandler : IMenuInputHandler
{
    private readonly IUserActionLogger _userActionLogger;

    public WriteToSupportMenuInputHandler(
        IUserActionLogger userActionLogger)
    {
        _userActionLogger = userActionLogger;
    }

    public MenuKey Key => MenuKey.Support;
        
    public ValueTask<MenuInputHandlingServiceResult> Handle(MessageModel message)
    {
        _userActionLogger.LogWriteToSupport(message.User, message.Text);

        var reply = new ReplyResourceResult(ResourceKey.Dialog_SupportReply);
        var result = MenuInputHandlingServiceResult.Success(reply); 
            
        return ValueTask.FromResult(result);
    }
}