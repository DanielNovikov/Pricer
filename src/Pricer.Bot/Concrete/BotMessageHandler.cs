using Pricer.Bot.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Bot.Concrete;

public class BotMessageHandler : IBotMessageHandler
{
    private readonly IMessageHandler _messageHandler;
    private readonly IResourceService _resourceService;

    public BotMessageHandler(
        IMessageHandler messageHandler,
        IResourceService resourceService)
    {
        _messageHandler = messageHandler;
        _resourceService = resourceService;
    }

    public async Task Handle(MessageHandlingModel messageHandlingModel, IBotService botService)
    {
        var serviceResult = await _messageHandler.Handle(messageHandlingModel);

        var userExternalId = messageHandlingModel.User.ExternalId;
        if (!serviceResult.IsSuccess)
        {
            var errorMessage = _resourceService.Get(serviceResult.Error);
            await botService.SendText(userExternalId, errorMessage);
            return;
        }

        switch (serviceResult.Result)
        {
            case ReplyKeyboardResult keyboardResult:
            {
                var text = _resourceService.Get(keyboardResult.Resource, keyboardResult.Parameters);
                
                var keyboard = keyboardResult.Keyboard switch
                {
                    MessageKeyboard messageKeyboard => botService.SendTextWithMessageKeyboard(userExternalId, text, messageKeyboard),
                    MenuKeyboard menuKeyboard => botService.SendTextWithMenuKeyboard(userExternalId, text, menuKeyboard),
                    _ => throw new InvalidOperationException(
                        $"Unexpected type of reply result {keyboardResult.Keyboard.GetType().FullName}")
                };
                
                break;
            }
            case ReplyResourceResult resourceResult:
            {
                var text = _resourceService.Get(resourceResult.Resource, resourceResult.Parameters);
                await botService.SendText(userExternalId, text);
                break;
            }
            case ReplyTextResult textResult:
            {
                await botService.SendText(userExternalId, textResult.Text);
                break;
            }
            default:
                throw new InvalidOperationException($"Unexpected type of reply result {serviceResult.Result.GetType().FullName}");
        }
    }
}