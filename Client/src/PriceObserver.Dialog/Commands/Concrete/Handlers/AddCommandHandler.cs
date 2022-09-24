using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Commands.Concrete.Handlers;

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