using System;
using System.Threading.Tasks;
using Pricer.Bot.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Callbacks.Abstract;
using Pricer.Dialog.Models;
using Pricer.Telegram.Abstract;

namespace Pricer.Telegram.Concrete;

public class TelegramCallbackHandler : ITelegramCallbackHandler
{
    private readonly ICallbackHandlerService _callbackHandlerService;
    private readonly IResourceService _resourceService;
    private readonly ITelegramBotService _telegramBotService;

    public TelegramCallbackHandler(
        ICallbackHandlerService callbackHandlerService,
        IResourceService resourceService, 
        ITelegramBotService telegramBotService)
    {
        _callbackHandlerService = callbackHandlerService;
        _resourceService = resourceService;
        _telegramBotService = telegramBotService;
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
                        await _telegramBotService.DeleteMessage(userExternalId, messageId);
                        await _telegramBotService.SendTextWithMenuKeyboard(userExternalId, text, menuKeyboard);
                        return;
                    case MessageKeyboard messageKeyboard:
                        await _telegramBotService.EditMessageWithKeyboard(userExternalId, messageId, text, messageKeyboard);
                        return;
                    default:
                        throw new InvalidOperationException($"Unexpected type of reply result {keyboardResult.Keyboard.GetType().FullName}");
                }
            }
            case ReplyResourceResult resourceResult:
            {
                var text = _resourceService.Get(resourceResult.Resource, resourceResult.Parameters);
                await _telegramBotService.EditMessage(userExternalId, messageId, text);
                break;
            }
            case ReplyTextResult textResult:
            {
                await _telegramBotService.EditMessage(userExternalId, messageId, textResult.Text);
                break;
            }
            default:
                throw new InvalidOperationException($"Unexpected type of reply result {serviceResult.Result.GetType().FullName}");
        }
    }
}