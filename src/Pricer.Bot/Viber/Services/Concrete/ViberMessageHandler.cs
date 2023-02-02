using Pricer.Bot.Viber.Services.Abstract;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Bot.Viber.Services.Concrete;

public class ViberMessageHandler : IViberMessageHandler
{
    private readonly IMessageHandler _messageHandler;
    private readonly IResourceService _resourceService;
    private readonly IViberBotService _viberBotService;
    private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;
    private readonly IUserRepository _userRepository;
    private readonly IMenuRepository _menuRepository;

    public ViberMessageHandler(
        IMessageHandler messageHandler,
        IResourceService resourceService, 
        IViberBotService viberBotService, 
        IMenuKeyboardBuilder menuKeyboardBuilder, 
        IUserRepository userRepository, 
        IMenuRepository menuRepository)
    {
        _messageHandler = messageHandler;
        _resourceService = resourceService;
        _viberBotService = viberBotService;
        _menuKeyboardBuilder = menuKeyboardBuilder;
        _userRepository = userRepository;
        _menuRepository = menuRepository;
    }
    
    public async Task Handle(MessageHandlingModel messageHandlingModel)
    {
        var serviceResult = await _messageHandler.Handle(messageHandlingModel);

        var userExternalId = messageHandlingModel.User.ExternalId;
        if (!serviceResult.IsSuccess)
        {
            var errorMessage = _resourceService.Get(serviceResult.Error);
            var user = await _userRepository.GetByExternalId(userExternalId);
            var keyboard = _menuKeyboardBuilder.Build(user.MenuKey);
            await _viberBotService.SendTextWithMenuKeyboard(userExternalId, errorMessage, keyboard);
            return;
        }

        switch (serviceResult.Result)
        {
            case ReplyKeyboardResult keyboardResult:
            {
                var text = _resourceService.Get(keyboardResult.Resource, keyboardResult.Parameters);

                switch (keyboardResult.Keyboard)
                {
                    case MessageKeyboard messageKeyboard:
                        await _viberBotService.SendTextWithMessageKeyboard(userExternalId, text, messageKeyboard);
                        
                        var user = await _userRepository.GetByExternalId(userExternalId);
                        var menu = _menuRepository.GetByKey(user.MenuKey);
                        var keyboard = _menuKeyboardBuilder.Build(menu.Key);
                        var menuText = _resourceService.Get(menu.Title);
                        
                        await _viberBotService.SendTextWithMenuKeyboard(userExternalId, menuText, keyboard);
                        break;
                    case MenuKeyboard menuKeyboard:
                        await _viberBotService.SendTextWithMenuKeyboard(userExternalId, text, menuKeyboard);
                        break;
                    default:
                        throw new InvalidOperationException(
                            $"Unexpected type of reply result {keyboardResult.Keyboard.GetType().FullName}");
                }

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