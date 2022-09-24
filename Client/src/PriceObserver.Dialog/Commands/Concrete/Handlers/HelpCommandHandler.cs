using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Commands.Concrete.Handlers;

public class HelpCommandHandler : ICommandHandler
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly ICommandService _commandService;
        
    public HelpCommandHandler(
        IUserActionLogger userActionLogger,
        ICommandService commandService)
    {
        _userActionLogger = userActionLogger;
        _commandService = commandService;
    }

    public CommandKey Key => CommandKey.Help;
        
    public ValueTask<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogHelpCalled(user);

        var addCommandTitle = _commandService.GetTitle(CommandKey.Add);
        var allItemsCommandTitle = _commandService.GetTitle(CommandKey.AllItems);
        var shopsCommandTitle = _commandService.GetTitle(CommandKey.Shops);
        var websiteCommandTitle = _commandService.GetTitle(CommandKey.Website);
        var writeToSupportCommandTitle = _commandService.GetTitle(CommandKey.WriteToSupport);

        var result = new ReplyResourceResult(
            ResourceKey.Dialog_Help,
            addCommandTitle, allItemsCommandTitle, shopsCommandTitle, websiteCommandTitle, writeToSupportCommandTitle);
        
        var serviceResult = CommandHandlingServiceResult.Success(result);

        return ValueTask.FromResult(serviceResult);
    }
}