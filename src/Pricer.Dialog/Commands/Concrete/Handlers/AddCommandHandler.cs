using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using Pricer.Dialog.Commands.Abstract;
using Pricer.Dialog.Models;
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
    
    public ValueTask<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogGotAddItemInstruction(user);
        
        var replyResult = new ReplyResourceResult(ResourceKey.Dialog_AddItemInformation);
        var serviceResult = CommandHandlingServiceResult.Success(replyResult);

        return ValueTask.FromResult(serviceResult);
    }
}