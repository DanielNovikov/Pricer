using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;
using Pricer.Viber.Services.Abstract;

namespace Pricer.Viber.Services.Concrete;

public class ViberMessageHandler : IViberMessageHandler
{
    private readonly IMessageHandler _telegramMessageHandler;
    private readonly IResourceService _resourceService;
    private readonly IViberBotService _viberBotService;
    private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;
    private readonly IUserRepository _userRepository;

    public ViberMessageHandler(
        IMessageHandler telegramMessageHandler,
        IResourceService resourceService, 
        IViberBotService viberBotService, 
        IMenuKeyboardBuilder menuKeyboardBuilder, 
        IUserRepository userRepository)
    {
        _telegramMessageHandler = telegramMessageHandler;
        _resourceService = resourceService;
        _viberBotService = viberBotService;
        _menuKeyboardBuilder = menuKeyboardBuilder;
        _userRepository = userRepository;
    }
    
    public async Task Handle(MessageHandlingModel messageHandlingModel)
    {
        var serviceResult = await _telegramMessageHandler.Handle(messageHandlingModel);

        var userExternalId = messageHandlingModel.User.ExternalId;
        if (!serviceResult.IsSuccess)
        {
            var errorMessage = _resourceService.Get(serviceResult.Error);
            await _viberBotService.SendText(userExternalId, errorMessage);
            return;
        }

        switch (serviceResult.Result)
        {
            case ReplyKeyboardResult keyboardResult:
            {
                var text = _resourceService.Get(keyboardResult.Resource, keyboardResult.Parameters);
                
                _ = keyboardResult.Keyboard switch
                {
                    MessageKeyboard messageKeyboard => _viberBotService.SendTextWithMessageKeyboard(userExternalId, text, messageKeyboard),
                    MenuKeyboard menuKeyboard => _viberBotService.SendTextWithMenuKeyboard(userExternalId, text, menuKeyboard),
                    _ => throw new InvalidOperationException(
                        $"Unexpected type of reply result {keyboardResult.Keyboard.GetType().FullName}")
                };
                
                break;
            }
            case ReplyResourceResult resourceResult:
            {
                var text = _resourceService.Get(resourceResult.Resource, resourceResult.Parameters);
                var user = await _userRepository.GetByExternalId(userExternalId);
                var keyboard = _menuKeyboardBuilder.Build(user.MenuKey);
                
                await _viberBotService.SendTextWithMenuKeyboard(userExternalId, text, keyboard);
                break;
            }
            case ReplyTextResult textResult:
            {
                var user = await _userRepository.GetByExternalId(userExternalId);
                var keyboard = _menuKeyboardBuilder.Build(user.MenuKey);
                
                await _viberBotService.SendTextWithMenuKeyboard(userExternalId, textResult.Text, keyboard);
                break;
            }
            default:
                throw new InvalidOperationException($"Unexpected type of reply result {serviceResult.Result.GetType().FullName}");
        }
    }
}