﻿using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Input.Commands.Abstract;
using Pricer.Dialog.Input.Menus.Abstract;
using Pricer.Dialog.Input.Models;
using Pricer.Dialog.Input.Services.Abstract;

namespace Pricer.Dialog.Input.Services.Concrete;

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

    public async Task<MessageHandlingResult> Handle(MessageHandlingModel messageHandlingModel)
    {
        var userModel = messageHandlingModel.User;
        
        var user = await _authorizationService.LogIn(userModel.ExternalId);
        var text = messageHandlingModel.Text;

        if (user is null)
        {
            var replyWithKeyboardResult = await _userRegistrationHandler.Handle(userModel);
            return MessageHandlingResult.Success(replyWithKeyboardResult);
        }
        
        var message = new MessageModel(text, user);
            
        var command = _commandService.GetByTitle(message.Text);
        if (command is null)
        {
            var menuInputHandlingServiceResult = await _menuInputHandlerService.Handle(message);
            return MessageHandlingResult.FromServiceResult(menuInputHandlingServiceResult);
        }

        var commandHandlingServiceResult = await _commandHandlerService.Handle(command, message);
        return MessageHandlingResult.FromServiceResult(commandHandlingServiceResult);
    }
}