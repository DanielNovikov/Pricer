using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Commands.Concrete.HelpCommand;

public class HelpCommandHandler : ICommandHandler
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly IResourceService _resourceService;
    private readonly ICommandService _commandService;
        
    public HelpCommandHandler(
        IUserActionLogger userActionLogger,
        IResourceService resourceService, 
        ICommandService commandService)
    {
        _userActionLogger = userActionLogger;
        _resourceService = resourceService;
        _commandService = commandService;
    }

    public CommandKey Type => CommandKey.Help;
        
    public Task<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogHelpCalled(user);

        var addCommandTitle = _commandService.GetTitle(CommandKey.Add);
        var allItemsCommandTitle = _commandService.GetTitle(CommandKey.AllItems);
        var shopsCommandTitle = _commandService.GetTitle(CommandKey.Shops);
        var websiteCommandTitle = _commandService.GetTitle(CommandKey.Website);
        var writeToSupportCommandTitle = _commandService.GetTitle(CommandKey.WriteToSupport);

        var message = _resourceService.Get(
            ResourceKey.Dialog_Help,
            addCommandTitle,
            allItemsCommandTitle,
            shopsCommandTitle,
            websiteCommandTitle,
            writeToSupportCommandTitle);

        var result = ReplyResult.Reply(message);
        var serviceResult = CommandHandlingServiceResult.Success(result);

        return Task.FromResult(serviceResult);
    }
}