using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Menus.Concrete.Handlers;

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