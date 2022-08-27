using System.Threading.Tasks;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Callbacks.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Telegram.Abstract;
using PriceObserver.Telegram.Extensions;
using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PriceObserver.Telegram.Concrete;

public class UpdateHandler : IUpdateHandler
{
    private readonly IMessageHandler _messageHandler;
    private readonly ITelegramBotService _telegramBotService;
    private readonly IReplyKeyboardMarkupBuilder _keyboardBuilder;
    private readonly IResourceService _resourceService;
    private readonly ICallbackHandlerService _callbackHandlerService;
    private readonly IInlineKeyboardMarkupBuilder _inlineKeyboardMarkupBuilder;
        
    public UpdateHandler(
        IMessageHandler messageHandler, 
        ITelegramBotService telegramBotService,
        IReplyKeyboardMarkupBuilder keyboardBuilder, 
        IResourceService resourceService,
        ICallbackHandlerService callbackHandlerService,
        IInlineKeyboardMarkupBuilder inlineKeyboardMarkupBuilder)
    {
        _messageHandler = messageHandler;
        _telegramBotService = telegramBotService;
        _keyboardBuilder = keyboardBuilder;
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

        var result = serviceResult.Result;
        var keyboard = _inlineKeyboardMarkupBuilder.Build(result.Keyboard);
        
        var userExternalId = callback.User.ExternalId;
        var messageId = callback.MessageId;

        await _telegramBotService.EditMessage(userExternalId, messageId, result.MessageText, keyboard);
    }

    private async Task HandleMessage(MessageHandlingModel message)
    {
        var serviceResult = await _messageHandler.Handle(message);

        var userExternalId = message.User.ExternalId;
        if (!serviceResult.IsSuccess)
        {
            var errorMessage = _resourceService.Get(serviceResult.Error);
            await _telegramBotService.SendMessage(userExternalId, errorMessage);
            return;
        }

        var result = serviceResult.Result;
        var hasKeyboard = result.MenuKeyboard is not null;
        if (hasKeyboard)
        {
            var keyboard = _keyboardBuilder.Build(result.MenuKeyboard);
            await _telegramBotService.SendMessageWithReplyMarkup(userExternalId, result.Message, keyboard);
            return;
        }

        await _telegramBotService.SendMessage(userExternalId, result.Message);
    }
}