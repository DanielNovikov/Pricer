using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Commands.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Commands.Concrete.Handlers;

public class AddCommandHandler : ICommandHandler
{
    private readonly IUserActionLogger _userActionLogger;

    public AddCommandHandler(IUserActionLogger userActionLogger)
    {
        _userActionLogger = userActionLogger;
    }

    public CommandKey Key => CommandKey.Add;
    
    public ValueTask<IReplyResult> Handle(User user)
    {
        _userActionLogger.LogGotAddItemInstruction(user);
        
        var result = new ReplyResourceResult(ResourceKey.Dialog_AddItemInformation);
        return ValueTask.FromResult<IReplyResult>(result);
    }
}