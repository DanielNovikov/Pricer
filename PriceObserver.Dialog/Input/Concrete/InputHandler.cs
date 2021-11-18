﻿using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Input.Abstract;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Model.Dialog.Input;

namespace PriceObserver.Dialog.Input.Concrete
{
    public class InputHandler : IInputHandler
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ICommandRepository _commandRepository;
        private readonly IMenuInputHandlerService _menuInputHandlerService;
        private readonly ICommandHandlerService _commandHandlerService;
        private readonly INewUserHandler _newUserHandler;
        
        public InputHandler(
            IAuthorizationService authorizationService, 
            ICommandRepository commandRepository,
            IMenuInputHandlerService menuInputHandlerService,
            ICommandHandlerService commandHandlerService,
            INewUserHandler newUserHandler)
        {
            _authorizationService = authorizationService;
            _commandRepository = commandRepository;
            _menuInputHandlerService = menuInputHandlerService;
            _commandHandlerService = commandHandlerService;
            _newUserHandler = newUserHandler;
        }

        public async Task<InputHandlingServiceResult> Handle(UpdateDto update)
        {
            var authorizationResult = await _authorizationService.Authorize(update);

            var text = update.Text;
            var user = authorizationResult.User;
            var message = new MessageDto(text, user);

            if (authorizationResult.IsNew)
            {
                var replyWithKeyboardResult = await _newUserHandler.Handle(message.User);
                return InputHandlingServiceResult.Success(replyWithKeyboardResult);
            }
            
            var command = await _commandRepository.GetByTitle(message.Text);
            if (command == null)
            {
                var menuInputHandlingServiceResult = await _menuInputHandlerService.Handle(message);
                return InputHandlingServiceResult.FromServiceResult(menuInputHandlingServiceResult);
            }

            var commandHandlingServiceResult = await _commandHandlerService.Handle(command, message);
            return InputHandlingServiceResult.FromServiceResult(commandHandlingServiceResult);
        }
    }
}