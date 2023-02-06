using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Menus.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Menus.Concrete.Handlers;

public class WriteToSupportMenuInputHandler : IMenuInputHandler
{
    private readonly IUserActionLogger _userActionLogger;

    public WriteToSupportMenuInputHandler(
        IUserActionLogger userActionLogger)
    {
        _userActionLogger = userActionLogger;
    }

    public MenuKey Key => MenuKey.Support;
        
    public ValueTask<IReplyResult> Handle(MessageModel message)
    {
        _userActionLogger.LogWriteToSupport(message.User, message.Text);

        var result = new ReplyResourceResult(ResourceKey.Dialog_SupportReply);
        return ValueTask.FromResult<IReplyResult>(result);
    }
}