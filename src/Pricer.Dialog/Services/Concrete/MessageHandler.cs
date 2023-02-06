using System.Threading.Tasks;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Commands.Abstract;
using Pricer.Dialog.Menus.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Services.Concrete;

public class MessageHandler : IMessageHandler
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IMenuInputHandlerService _menuInputHandlerService;
    private readonly ICommandHandlerService _commandHandlerService;
    private readonly IUserRegistrationHandler _userRegistrationHandler;
    private readonly ICommandService _commandService;
        
    public MessageHandler(
        IAuthorizationService authorizationService, 
        IMenuInputHandlerService menuInputHandlerService,
        ICommandHandlerService commandHandlerService,
        IUserRegistrationHandler userRegistrationHandler, 
        ICommandService commandService)
    {
        _authorizationService = authorizationService;
        _menuInputHandlerService = menuInputHandlerService;
        _commandHandlerService = commandHandlerService;
        _userRegistrationHandler = userRegistrationHandler;
        _commandService = commandService;
    }

    public async Task<IReplyResult> Handle(MessageHandlingModel messageHandlingModel)
    {
        var userModel = messageHandlingModel.User;
        
        var user = await _authorizationService.LogIn(userModel.ExternalId);
        var text = messageHandlingModel.Text;

        if (user is null)
            return await _userRegistrationHandler.Handle(userModel);
        
        var message = new MessageModel(text, user);
        var command = _commandService.GetByTitle(message.Text);
        if (command is null)
            return await _menuInputHandlerService.Handle(message);

        return await _commandHandlerService.Handle(command, message);
    }
}