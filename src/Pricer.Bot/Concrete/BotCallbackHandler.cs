using Pricer.Bot.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Callbacks.Abstract;
using Pricer.Dialog.Models;

namespace Pricer.Bot.Concrete;

public class BotCallbackHandler : IBotCallbackHandler
{
    private readonly ICallbackHandlerService _callbackHandlerService;
    private readonly IResourceService _resourceService;

    public BotCallbackHandler(
        ICallbackHandlerService callbackHandlerService,
        IResourceService resourceService)
    {
        _callbackHandlerService = callbackHandlerService;
        _resourceService = resourceService;
    }

    public async Task Handle(CallbackHandlingModel callbackHandlingModel, IBotService botService)
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
                        await botService.DeleteMessage(userExternalId, messageId);
                        await botService.SendTextWithMenuKeyboard(userExternalId, text, menuKeyboard);
                        return;
                    case MessageKeyboard messageKeyboard:
                        await botService.EditMessageWithKeyboard(userExternalId, messageId, text, messageKeyboard);
                        return;
                    default:
                        throw new InvalidOperationException($"Unexpected type of reply result {keyboardResult.Keyboard.GetType().FullName}");
                }
            }
            case ReplyResourceResult resourceResult:
            {
                var text = _resourceService.Get(resourceResult.Resource, resourceResult.Parameters);
                await botService.EditMessage(userExternalId, messageId, text);
                break;
            }
            case ReplyTextResult textResult:
            {
                await botService.EditMessage(userExternalId, messageId, textResult.Text);
                break;
            }
            default:
                throw new InvalidOperationException($"Unexpected type of reply result {serviceResult.Result.GetType().FullName}");
        }
    }
}