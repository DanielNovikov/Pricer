using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Input.Models;
using PriceObserver.Dialog.Menus.Abstract;

namespace PriceObserver.Dialog.Commands.Concrete
{
    public class CommandHandlerService : ICommandHandlerService
    {
        private readonly IEnumerable<ICommandHandler> _commandHandlers;
        private readonly IMenuCommandRepository _menuCommandRepository;
        private readonly IUserService _userService;
        private readonly IReplyWithKeyboardBuilder _replyWithKeyboardBuilder;
        private readonly IUserActionLogger _userActionLogger;

        public CommandHandlerService(
            IEnumerable<ICommandHandler> commandHandlers,
            IMenuCommandRepository menuCommandRepository, 
            IUserService userService,
            IReplyWithKeyboardBuilder replyWithKeyboardBuilder, 
            IUserActionLogger userActionLogger)
        {
            _commandHandlers = commandHandlers;
            _menuCommandRepository = menuCommandRepository;
            _userService = userService;
            _replyWithKeyboardBuilder = replyWithKeyboardBuilder;
            _userActionLogger = userActionLogger;
        }

        public async Task<CommandHandlingServiceResult> Handle(Command command, MessageDto message)
        {
            var user = message.User;
            var menu = user.Menu;

            if (menu.ParentId.HasValue && command.Type == CommandType.Back)
            {
                _userActionLogger.LogRedirectToMenu(user, menu.Parent);
                return await RedirectUserToMenu(user, menu.Parent);
            }

            var commandAvailableInMenu = await _menuCommandRepository.HasPair(menu.Id, command.Id);
            if (!commandAvailableInMenu)
            {
                _userActionLogger.LogWrongCommand(message.User, message.Text);
                return CommandHandlingServiceResult.Fail(ResourceKey.Dialog_IncorrectCommand);
            }

            if (command.MenuToRedirectId.HasValue)
            {
                _userActionLogger.LogRedirectToMenu(user, command.MenuToRedirect);
                return await RedirectUserToMenu(user, command.MenuToRedirect);
            }

            var commandHandler = _commandHandlers.First(x => x.Type == command.Type);
            return await commandHandler.Handle(user);
        }

        private async Task<CommandHandlingServiceResult> RedirectUserToMenu(User user, Menu menuToRedirect)
        {
            await _userService.RedirectToMenu(user, menuToRedirect);

            var replyWithKeyboardResult = await _replyWithKeyboardBuilder.Build(menuToRedirect);
            return CommandHandlingServiceResult.Success(replyWithKeyboardResult);
        }
    }
}