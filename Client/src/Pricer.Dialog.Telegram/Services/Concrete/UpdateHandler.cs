using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Callback.Models;
using Pricer.Dialog.Callback.Services.Abstract;
using Pricer.Dialog.Common.Models;
using Pricer.Dialog.Common.Models.Callback;
using Pricer.Dialog.Common.Models.Input;
using Pricer.Dialog.Input.Models;
using Pricer.Dialog.Input.Services.Abstract;
using Pricer.Dialog.Telegram.Extensions;
using Pricer.Dialog.Telegram.Services.Abstract;
using Pricer.Telegram.Abstract;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Pricer.Dialog.Telegram.Services.Concrete;

public class UpdateHandler : IUpdateHandler
{
    private readonly IMessageHandler _messageHandler;
    private readonly ITelegramBotService _telegramBotService;
    private readonly IReplyKeyboardMarkupBuilder _replyKeyboardBuilder;
    private readonly IResourceService _resourceService;
    private readonly ICallbackHandlerService _callbackHandlerService;
    private readonly IInlineKeyboardMarkupBuilder _inlineKeyboardMarkupBuilder;
        
    public UpdateHandler(
        IMessageHandler messageHandler, 
        ITelegramBotService telegramBotService,
        IReplyKeyboardMarkupBuilder replyKeyboardBuilder, 
        IResourceService resourceService,
        ICallbackHandlerService callbackHandlerService,
        IInlineKeyboardMarkupBuilder inlineKeyboardMarkupBuilder)
    {
        _messageHandler = messageHandler;
        _telegramBotService = telegramBotService;
        _replyKeyboardBuilder = replyKeyboardBuilder;
        _resourceService = resourceService;
        _callbackHandlerService = callbackHandlerService;
        _inlineKeyboardMarkupBuilder = inlineKeyboardMarkupBuilder;
    }

    public async Task Handle(Update update)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                await HandleMessage(update.ToMessage());
                return;
            case UpdateType.CallbackQuery:
                await HandleCallbackQuery(update.ToCallback());
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(update.Type));
        }
    }

    private async Task HandleCallbackQuery(CallbackHandlingModel callback)
    {
        var serviceResult = await _callbackHandlerService.Handle(callback);

        if (!serviceResult.IsSuccess)
            return;
        
        var userExternalId = callback.User.ExternalId;
        var messageId = callback.MessageId;
        
        switch (serviceResult.Result)
        {
            case ReplyKeyboardResult keyboardResult:
            {
                var message = _resourceService.Get(
                    keyboardResult.Resource,
                    keyboardResult.Parameters);
                
                if (keyboardResult.Keyboard is MenuKeyboard menuKeyboard)
                {
                    await _telegramBotService.DeleteMessage(userExternalId, messageId);
            
                    var replyKeyboard = _replyKeyboardBuilder.Build(menuKeyboard);
                    await _telegramBotService.SendMessageWithReplyMarkup(userExternalId, message, replyKeyboard);
                    return;
                }

                if (!(keyboardResult.Keyboard is MessageKeyboard messageKeyboard))
                    throw new InvalidOperationException(
                        $"Unexpected type of reply result {keyboardResult.Keyboard.GetType().FullName}");
                
                var inlineKeyboard = _inlineKeyboardMarkupBuilder.Build(messageKeyboard);
                await _telegramBotService.EditMessageWithInlineKeyboard(
                    userExternalId, messageId, message, inlineKeyboard);
                
                break;
            }
            case ReplyResourceResult resourceResult:
            {
                var message = _resourceService.Get(resourceResult.Resource, resourceResult.Parameters);
                await _telegramBotService.EditMessage(userExternalId, messageId, message);
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

    private async Task HandleMessage(MessageHandlingModel messageModel)
    {
        var serviceResult = await _messageHandler.Handle(messageModel);

        var userExternalId = messageModel.User.ExternalId;
        if (!serviceResult.IsSuccess)
        {
            var errorMessage = _resourceService.Get(serviceResult.Error);
            await _telegramBotService.SendMessage(userExternalId, errorMessage);
            return;
        }

        switch (serviceResult.Result)
        {
            case ReplyKeyboardResult keyboardResult:
            {
                var keyboard = keyboardResult.Keyboard switch
                {
                    MessageKeyboard messageKeyboard => _inlineKeyboardMarkupBuilder.Build(messageKeyboard),
                    MenuKeyboard menuKeyboard => _replyKeyboardBuilder.Build(menuKeyboard),
                    _ => throw new InvalidOperationException(
                        $"Unexpected type of reply result {keyboardResult.Keyboard.GetType().FullName}")
                };

                var message = _resourceService.Get(
                    keyboardResult.Resource,
                    keyboardResult.Parameters);
            
                await _telegramBotService.SendMessageWithReplyMarkup(userExternalId, message, keyboard);
                break;
            }
            case ReplyResourceResult resourceResult:
            {
                var message = _resourceService.Get(resourceResult.Resource, resourceResult.Parameters);
                await _telegramBotService.SendMessage(userExternalId, message);
                break;
            }
            case ReplyTextResult textResult:
            {
                await _telegramBotService.SendMessage(userExternalId, textResult.Text);
                break;
            }
            default:
                throw new InvalidOperationException($"Unexpected type of reply result {serviceResult.Result.GetType().FullName}");
        }
    }
}