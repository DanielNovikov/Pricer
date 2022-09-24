using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Common.Models.Callback;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Input.Commands.Abstract;
using Pricer.Dialog.Input.Models;

namespace Pricer.Dialog.Input.Commands.Concrete.Handlers;

public class AddCommandHandler : ICommandHandler
{
    private readonly IUserActionLogger _userActionLogger;

    public AddCommandHandler(IUserActionLogger userActionLogger)
    {
        _userActionLogger = userActionLogger;
    }

    public CommandKey Key => CommandKey.Add;
    
    public ValueTask<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogGotAddItemInstruction(user);
        
        var replyResult = new ReplyResourceResult(ResourceKey.Dialog_AddItemInformation);
        var serviceResult = CommandHandlingServiceResult.Success(replyResult);

        return ValueTask.FromResult(serviceResult);
    }
}