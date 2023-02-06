using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Dialog.Commands.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Commands.Concrete;

public class CommandHandlerService : ICommandHandlerService
{
    private readonly IEnumerable<ICommandHandler> _commandHandlers;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IMenuRepository _menuRepository;
    private readonly IUserRedirectionService _userRedirectionService;
    private readonly IWrongCommandHandler _wrongCommandHandler;

    public CommandHandlerService(
        IEnumerable<ICommandHandler> commandHandlers,
        IUserActionLogger userActionLogger, 
        IMenuRepository menuRepository, 
        IUserRedirectionService userRedirectionService,
        IWrongCommandHandler wrongCommandHandler)
    {
        _commandHandlers = commandHandlers;
        _userActionLogger = userActionLogger;
        _menuRepository = menuRepository;
        _userRedirectionService = userRedirectionService;
        _wrongCommandHandler = wrongCommandHandler;
    }

    public async Task<IReplyResult> Handle(Command command, MessageModel message)
    {
        var user = message.User;
        var menu = _menuRepository.GetByKey(user.MenuKey);

        if (menu.Parent is not null && command.Key == CommandKey.Back)
        {
            _userActionLogger.LogRedirectBackToMenu(user, menu.Parent);
            return await _userRedirectionService.Redirect(user, menu.Parent);
        }

        var commandAvailableInMenu = menu.Commands.Any(x => x == command);
        if (!commandAvailableInMenu)
            return _wrongCommandHandler.Handle(message);

        if (command.MenuToRedirect is not null)
        {            
            _userActionLogger.LogRedirectToMenu(user, command.MenuToRedirect);
            return await _userRedirectionService.Redirect(user, command.MenuToRedirect);
        }

        var commandHandler = _commandHandlers.First(x => x.Key == command.Key);
        return await commandHandler.Handle(user);
    }
}