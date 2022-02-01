using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Commands.Concrete.AddCommand;

public class AddCommandHandler : ICommandHandler
{
    private readonly IResourceService _resourceService;
    private readonly IUserActionLogger _userActionLogger;

    public AddCommandHandler(
        IResourceService resourceService,
        IUserActionLogger userActionLogger)
    {
        _resourceService = resourceService;
        _userActionLogger = userActionLogger;
    }

    public CommandKey Type => CommandKey.Add;
    
    public Task<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogGotAddItemInstruction(user);
        
        var message = _resourceService.Get(ResourceKey.Dialog_AddItemInformation);

        var replyResult = ReplyResult.Reply(message);
        var serviceResult = CommandHandlingServiceResult.Success(replyResult);

        return Task.FromResult(serviceResult);
    }
}