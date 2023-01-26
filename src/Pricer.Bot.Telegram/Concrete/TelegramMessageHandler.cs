using System;
using System.Threading.Tasks;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;
using Pricer.Telegram.Abstract;

namespace Pricer.Telegram.Concrete;

public class TelegramMessageHandler : ITelegramMessageHandler
{
    private readonly IMessageHandler _telegramMessageHandler;
    private readonly IResourceService _resourceService;
    private readonly ITelegramBotService _telegramBotService;

    public TelegramMessageHandler(
        IMessageHandler telegramMessageHandler,
        IResourceService resourceService, 
        ITelegramBotService telegramBotService)
    {
        _telegramMessageHandler = telegramMessageHandler;
        _resourceService = resourceService;
        _telegramBotService = telegramBotService;
    }
    
    public async Task Handle(MessageHandlingModel messageHandlingModel)
    {
        var serviceResult = await _telegramMessageHandler.Handle(messageHandlingModel);

        var userExternalId = messageHandlingModel.User.ExternalId;
        if (!serviceResult.IsSuccess)
        {
            var errorMessage = _resourceService.Get(serviceResult.Error);
            await _telegramBotService.SendText(userExternalId, errorMessage);
            return;
        }

        switch (serviceResult.Result)
        {
            case ReplyKeyboardResult keyboardResult:
            {
                var text = _resourceService.Get(keyboardResult.Resource, keyboardResult.Parameters);
                
                _ = keyboardResult.Keyboard switch
                {
                    MessageKeyboard messageKeyboard => _telegramBotService.SendTextWithMessageKeyboard(userExternalId, text, messageKeyboard),
                    MenuKeyboard menuKeyboard => _telegramBotService.SendTextWithMenuKeyboard(userExternalId, text, menuKeyboard),
                    _ => throw new InvalidOperationException(
                        $"Unexpected type of reply result {keyboardResult.Keyboard.GetType().FullName}")
                };
                
                break;
            }
            case ReplyResourceResult resourceResult:
            {
                var text = _resourceService.Get(resourceResult.Resource, resourceResult.Parameters);
                await _telegramBotService.SendText(userExternalId, text);
                break;
            }
            case ReplyTextResult textResult:
            {
                await _telegramBotService.SendText(userExternalId, textResult.Text);
                break;
            }
            default:
                throw new InvalidOperationException($"Unexpected type of reply result {serviceResult.Result.GetType().FullName}");
        }
    }
}