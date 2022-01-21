using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Commands.Concrete;

public class CommandHandlerService : ICommandHandlerService
{
    private readonly IEnumerable<ICommandHandler> _commandHandlers;
    private readonly IUserService _userService;
    private readonly IReplyWithKeyboardBuilder _replyWithKeyboardBuilder;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IMenuRepository _menuRepository;

    public CommandHandlerService(
        IEnumerable<ICommandHandler> commandHandlers,
        IUserService userService,
        IReplyWithKeyboardBuilder replyWithKeyboardBuilder, 
        IUserActionLogger userActionLogger, 
        IMenuRepository menuRepository)
    {
        _commandHandlers = commandHandlers;
        _userService = userService;
        _replyWithKeyboardBuilder = replyWithKeyboardBuilder;
        _userActionLogger = userActionLogger;
        _menuRepository = menuRepository;
    }

    public async Task<CommandHandlingServiceResult> Handle(Command command, MessageServiceModel message)
    {
        var user = message.User;
        var menu = _menuRepository.GetByKey(user.MenuKey);

        if (menu.Parent is not null && command.Key == CommandKey.Back)
        {
            _userActionLogger.LogRedirectToMenu(user, menu.Parent);
            return await RedirectUserToMenu(user, menu.Parent);
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
            return await RedirectUserToMenu(user, command.MenuToRedirect);
        }

        var commandHandler = _commandHandlers.First(x => x.Type == command.Key);
        return await commandHandler.Handle(user);
    }

    private async Task<CommandHandlingServiceResult> RedirectUserToMenu(User user, Menu menuToRedirect)
    {
        await _userService.RedirectToMenu(user, menuToRedirect);

        var replyWithKeyboardResult = await _replyWithKeyboardBuilder.Build(menuToRedirect);
        return CommandHandlingServiceResult.Success(replyWithKeyboardResult);
    }
}