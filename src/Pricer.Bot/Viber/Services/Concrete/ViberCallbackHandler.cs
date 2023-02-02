using Pricer.Bot.Viber.Services.Abstract;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Callbacks.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Bot.Viber.Services.Concrete;

public class ViberCallbackHandler : IViberCallbackHandler
{
    private readonly ICallbackHandlerService _callbackHandlerService;
    private readonly IResourceService _resourceService;
    private readonly IViberBotService _viberBotService;
    private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;
    private readonly IUserRepository _userRepository;
    private readonly IMenuRepository _menuRepository;

    public ViberCallbackHandler(
        ICallbackHandlerService callbackHandlerService,
        IResourceService resourceService, 
        IViberBotService viberBotService,
        IMenuKeyboardBuilder menuKeyboardBuilder, 
        IUserRepository userRepository, 
        IMenuRepository menuRepository)
    {
        _callbackHandlerService = callbackHandlerService;
        _resourceService = resourceService;
        _viberBotService = viberBotService;
        _menuKeyboardBuilder = menuKeyboardBuilder;
        _userRepository = userRepository;
        _menuRepository = menuRepository;
    }

    public async Task Handle(CallbackHandlingModel callbackHandlingModel)
    {
        var serviceResult = await _callbackHandlerService.Handle(callbackHandlingModel);

        if (!serviceResult.IsSuccess)
            return;
        
        var userExternalId = callbackHandlingModel.User.ExternalId;
        
        switch (serviceResult.Result)
        {
            case ReplyKeyboardResult keyboardResult:
            {
                var text = _resourceService.Get(keyboardResult.Resource, keyboardResult.Parameters);
                
                switch (keyboardResult.Keyboard)
                {
                    case MenuKeyboard menuKeyboard:
                        await _viberBotService.SendTextWithMenuKeyboard(userExternalId, text, menuKeyboard);
                        return;
                    case MessageKeyboard messageKeyboard:
                        await _viberBotService.SendTextWithMessageKeyboard(userExternalId, text, messageKeyboard);
                        
                        var user = await _userRepository.GetByExternalId(userExternalId);
                        var menu = _menuRepository.GetByKey(user.MenuKey);
                        var keyboard = _menuKeyboardBuilder.Build(menu.Key);
                        var menuText = _resourceService.Get(menu.Title);
                        
                        await _viberBotService.SendTextWithMenuKeyboard(userExternalId, menuText, keyboard);
                        return;
                    default:
                        throw new InvalidOperationException($"Unexpected type of reply result {keyboardResult.Keyboard.GetType().FullName}");
                }
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