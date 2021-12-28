using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Input.Abstract;
using PriceObserver.Dialog.Input.Models;
using PriceObserver.Dialog.Menus.Abstract;

namespace PriceObserver.Dialog.Input.Concrete;

public class InputHandler : IInputHandler
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ICommandRepository _commandRepository;
    private readonly IMenuInputHandlerService _menuInputHandlerService;
    private readonly ICommandHandlerService _commandHandlerService;
    private readonly IUserRegistrationHandler _userRegistrationHandler;
    private readonly ICommandService _commandService;
        
    public InputHandler(
        IAuthorizationService authorizationService, 
        ICommandRepository commandRepository,
        IMenuInputHandlerService menuInputHandlerService,
        ICommandHandlerService commandHandlerService,
        IUserRegistrationHandler userRegistrationHandler, 
        ICommandService commandService)
    {
        _authorizationService = authorizationService;
        _commandRepository = commandRepository;
        _menuInputHandlerService = menuInputHandlerService;
        _commandHandlerService = commandHandlerService;
        _userRegistrationHandler = userRegistrationHandler;
        _commandService = commandService;
    }

    public async Task<InputHandlingServiceResult> Handle(UpdateDto update)
    {
        var authorizationResult = await _authorizationService.Authorize(update);

        var text = update.Text;
        var user = authorizationResult.User;
        var message = new MessageDto(text, user);

        if (authorizationResult.IsNew)
        {
            var replyWithKeyboardResult = await _userRegistrationHandler.Handle(message.User);
            return InputHandlingServiceResult.Success(replyWithKeyboardResult);
        }
            
        var command = _commandService.GetByTitle(message.Text);
        if (command is null)
        {
            var menuInputHandlingServiceResult = await _menuInputHandlerService.Handle(message);
            return InputHandlingServiceResult.FromServiceResult(menuInputHandlingServiceResult);
        }

        var commandHandlingServiceResult = await _commandHandlerService.Handle(command, message);
        return InputHandlingServiceResult.FromServiceResult(commandHandlingServiceResult);
    }
}