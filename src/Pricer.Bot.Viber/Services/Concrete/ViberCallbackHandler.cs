using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Callbacks.Abstract;
using Pricer.Dialog.Models;
using Pricer.Viber.Services.Abstract;

namespace Pricer.Viber.Services.Concrete;

public class ViberCallbackHandler : IViberCallbackHandler
{
    private readonly ICallbackHandlerService _callbackHandlerService;
    private readonly IResourceService _resourceService;
    private readonly IViberBotService _viberBotService;

    public ViberCallbackHandler(
        ICallbackHandlerService callbackHandlerService,
        IResourceService resourceService, 
        IViberBotService viberBotService)
    {
        _callbackHandlerService = callbackHandlerService;
        _resourceService = resourceService;
        _viberBotService = viberBotService;
    }

    public async Task Handle(CallbackHandlingModel callbackHandlingModel)
    {
        var serviceResult = await _callbackHandlerService.Handle(callbackHandlingModel);

        if (!serviceResult.IsSuccess)
            return;
        
        var userExternalId = callbackHandlingModel.User.ExternalId;
        var messageId = callbackHandlingModel.MessageId;
        
        switch (serviceResult.Result)
        {
            case ReplyKeyboardResult keyboardResult:
            {
                var text = _resourceService.Get(keyboardResult.Resource, keyboardResult.Parameters);
                
                switch (keyboardResult.Keyboard)
                {
                    case MenuKeyboard menuKeyboard:
                        await _viberBotService.DeleteMessage(userExternalId, messageId);
                        await _viberBotService.SendTextWithMenuKeyboard(userExternalId, text, menuKeyboard);
                        return;
                    case MessageKeyboard messageKeyboard:
                        await _viberBotService.EditMessageWithKeyboard(userExternalId, messageId, text, messageKeyboard);
                        return;
                    default:
                        throw new InvalidOperationException($"Unexpected type of reply result {keyboardResult.Keyboard.GetType().FullName}");
                }
            }
            case ReplyResourceResult resourceResult:
            {
                var text = _resourceService.Get(resourceResult.Resource, resourceResult.Parameters);
                await _viberBotService.EditMessage(userExternalId, messageId, text);
                break;
            }
            case ReplyTextResult textResult:
            {
                await _viberBotService.EditMessage(userExternalId, messageId, textResult.Text);
                break;
            }
            default:
                throw new InvalidOperationException($"Unexpected type of reply result {serviceResult.Result.GetType().FullName}");
        }
    }
}