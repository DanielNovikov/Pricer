using System;
using System.Threading.Tasks;
using Pricer.Bot.Abstract;
using Pricer.Telegram.Abstract;
using Pricer.Telegram.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Pricer.Telegram.Concrete;

public class UpdateHandler : IUpdateHandler
{
    private readonly IBotCallbackHandler _callbackHandler;
    private readonly IBotMessageHandler _messageHandler;
    private readonly ITelegramBotService _telegramBotService;

    public UpdateHandler(
        IBotCallbackHandler callbackHandler,
        IBotMessageHandler messageHandler, 
        ITelegramBotService telegramBotService)
    {
        _callbackHandler = callbackHandler;
        _messageHandler = messageHandler;
        _telegramBotService = telegramBotService;
    }

    public async Task Handle(Update update)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                await _messageHandler.Handle(update.ToMessage(), _telegramBotService);
                return;
            case UpdateType.CallbackQuery:
                await _callbackHandler.Handle(update.ToCallback(), _telegramBotService);
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(update.Type));
        }
    }
}