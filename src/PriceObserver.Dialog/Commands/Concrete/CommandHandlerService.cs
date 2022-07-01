using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;

namespace PriceObserver.Dialog.Commands.Concrete;

public class CommandHandlerService : ICommandHandlerService
{
    private readonly IEnumerable<ICommandHandler> _commandHandlers;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IMenuRepository _menuRepository;
    private readonly IUserRedirectionService _userRedirectionService;

    public CommandHandlerService(
        IEnumerable<ICommandHandler> commandHandlers,
        IUserActionLogger userActionLogger, 
        IMenuRepository menuRepository, 
        IUserRedirectionService userRedirectionService)
    {
        _commandHandlers = commandHandlers;
        _userActionLogger = userActionLogger;
        _menuRepository = menuRepository;
        _userRedirectionService = userRedirectionService;
    }

    public async Task<CommandHandlingServiceResult> Handle(Command command, MessageServiceModel message)
    {
        var user = message.User;
        var menu = _menuRepository.GetByKey(user.MenuKey);

        if (menu.Parent is not null && command.Key == CommandKey.Back)
        {
            var replyResult = await _userRedirectionService.Redirect(user, menu.Parent);
            return CommandHandlingServiceResult.Success(replyResult);
        }

        var commandAvailableInMenu = menu.Commands.Any(x => x == command);
        if (!commandAvailableInMenu)
        {
            _userActionLogger.LogWrongCommand(message.User, message.Text);
            return CommandHandlingServiceResult.Fail(ResourceKey.Dialog_IncorrectCommand);
        }

        if (command.MenuToRedirect is not null)
        {            
            var replyResult = await _userRedirectionService.Redirect(user, command.MenuToRedirect);
            return CommandHandlingServiceResult.Success(replyResult);
        }

        var commandHandler = _commandHandlers.First(x => x.Type == command.Key);
        return await commandHandler.Handle(user);
    }
}