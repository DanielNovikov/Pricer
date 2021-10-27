using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Telegram.Dialog.Commands.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Commands.Concrete
{
    public class CommandHandlerService : ICommandHandlerService
    {
        private readonly IEnumerable<ICommandHandler> _commandHandlers;
        private readonly IMenuCommandRepository _menuCommandRepository;
        private readonly IUserService _userService;

        public CommandHandlerService(
            IEnumerable<ICommandHandler> commandHandlers,
            IMenuCommandRepository menuCommandRepository, 
            IUserService userService)
        {
            _commandHandlers = commandHandlers;
            _menuCommandRepository = menuCommandRepository;
            _userService = userService;
        }

        public async Task<CommandHandlingServiceResult> Handle(Command command, Update update, User user)
        {
            var commandAvailableInMenu = await _menuCommandRepository.HasPair(user.Menu.Id, command.Id);
            if (!commandAvailableInMenu)
                return CommandHandlingServiceResult.Fail("Неверная комманда");

            if (command.MenuToRedirectId.HasValue)
            {
                await _userService.RedirectToMenu(user, command.MenuToRedirect);
                return CommandHandlingServiceResult.Success(); // return new cards
            }

            var commandHandler = _commandHandlers.First(x => x.Type == command.Type);
            return await commandHandler.Handle(update, user);
        }
    }
}