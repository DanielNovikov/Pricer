using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Model.Telegram.Input;
using PriceObserver.Model.Telegram.Menu;
using PriceObserver.Telegram.Dialog.Commands.Abstract;
using PriceObserver.Telegram.Dialog.Common.Abstract;
using PriceObserver.Telegram.Dialog.Common.Extensions;
using PriceObserver.Telegram.Dialog.Input.Abstract;
using PriceObserver.Telegram.Dialog.Menus.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Input.Concrete
{
    public class InputHandler : IInputHandler
    {
        private readonly IChatService _chatService;
        private readonly ICommandRepository _commandRepository;
        private readonly IMenuInputHandlerService _menuInputHandlerService;
        private readonly IMenuCommandRepository _menuCommandRepository;
        private readonly IUserService _userService;
        private readonly ICommandHandlerService _commandHandlerService;
        
        public InputHandler(
            IChatService chatService, 
            ICommandRepository commandRepository,
            IMenuInputHandlerService menuInputHandlerService,
            IMenuCommandRepository menuCommandRepository,
            IUserService userService,
            ICommandHandlerService commandHandlerService)
        {
            _chatService = chatService;
            _commandRepository = commandRepository;
            _menuInputHandlerService = menuInputHandlerService;
            _menuCommandRepository = menuCommandRepository;
            _userService = userService;
            _commandHandlerService = commandHandlerService;
        }

        public async Task<InputHandlingServiceResult> Handle(Update update)
        {
            var user = await GetUser(update);
            var command = await _commandRepository.GetByTitle(update.GetMessageText());

            if (command == null)
            {
                var menuInputHandlingServiceResult = await _menuInputHandlerService.Handle(update, user);
                return InputHandlingServiceResult.FromServiceResult(menuInputHandlingServiceResult);
            }

            var commandHandlingServiceResult = await _commandHandlerService.Handle(command, update, user);
            return InputHandlingServiceResult.FromServiceResult(commandHandlingServiceResult);
        }

        private async Task<User> GetUser(Update update)
        {
            var chat = update.Message.Chat;
            return await _chatService.GetUser(chat);
        }
    }
}