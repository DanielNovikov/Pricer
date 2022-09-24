using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Input.Commands.Abstract;
using Pricer.Dialog.Input.Models;
using Pricer.Dialog.Input.Services.Abstract;

namespace Pricer.Dialog.Input.Commands.Concrete;

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

    public async Task<CommandHandlingServiceResult> Handle(Command command, MessageModel message)
    {
        var user = message.User;
        var menu = _menuRepository.GetByKey(user.MenuKey);

        if (menu.Parent is not null && command.Key == CommandKey.Back)
        {
            _userActionLogger.LogRedirectBackToMenu(user, menu.Parent);
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
            _userActionLogger.LogRedirectToMenu(user, command.MenuToRedirect);
            var replyResult = await _userRedirectionService.Redirect(user, command.MenuToRedirect);
            return CommandHandlingServiceResult.Success(replyResult);
        }

        var commandHandler = _commandHandlers.First(x => x.Key == command.Key);
        return await commandHandler.Handle(user);
    }
}