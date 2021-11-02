using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Telegram.Common;
using PriceObserver.Model.Telegram.Input;
using PriceObserver.Telegram.Dialog.Commands.Abstract;
using PriceObserver.Telegram.Dialog.Common.Abstract;
using PriceObserver.Telegram.Dialog.Common.Extensions;
using PriceObserver.Telegram.Dialog.Input.Abstract;
using PriceObserver.Telegram.Dialog.Menus.Abstract;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Dialog.Input.Concrete
{
    public class InputHandler : IInputHandler
    {
        private readonly IChatAuthorizationService _chatAuthorizationService;
        private readonly ICommandRepository _commandRepository;
        private readonly IMenuInputHandlerService _menuInputHandlerService;
        private readonly ICommandHandlerService _commandHandlerService;
        private readonly INewUserHandler _newUserHandler;
        
        public InputHandler(
            IChatAuthorizationService chatAuthorizationService, 
            ICommandRepository commandRepository,
            IMenuInputHandlerService menuInputHandlerService,
            ICommandHandlerService commandHandlerService,
            INewUserHandler newUserHandler)
        {
            _chatAuthorizationService = chatAuthorizationService;
            _commandRepository = commandRepository;
            _menuInputHandlerService = menuInputHandlerService;
            _commandHandlerService = commandHandlerService;
            _newUserHandler = newUserHandler;
        }

        public async Task<InputHandlingServiceResult> Handle(Update update)
        {
            var authorizationResult = await Authorize(update);

            var user = authorizationResult.User;

            if (authorizationResult.IsNew)
            {
                var replyWithKeyboardResult = await _newUserHandler.Handle(user);
                return InputHandlingServiceResult.Success(replyWithKeyboardResult);
            }
            
            var command = await _commandRepository.GetByTitle(update.GetMessageText());
            if (command == null)
            {
                var menuInputHandlingServiceResult = await _menuInputHandlerService.Handle(update, user);
                return InputHandlingServiceResult.FromServiceResult(menuInputHandlingServiceResult);
            }

            var commandHandlingServiceResult = await _commandHandlerService.Handle(command, update, user);
            return InputHandlingServiceResult.FromServiceResult(commandHandlingServiceResult);
        }

        private async Task<AuthorizationResult> Authorize(Update update)
        {
            var chat = update.Message.Chat;
            return await _chatAuthorizationService.Authorize(chat);
        }
    }
}