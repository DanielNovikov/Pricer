using Pricer.Bot.Telegram.Abstract;
using Pricer.Bot.Telegram.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Pricer.Bot.Telegram.Concrete;

public class UpdateHandler : IUpdateHandler
{
    private readonly ITelegramCallbackHandler _callbackHandler;
    private readonly ITelegramMessageHandler _telegramMessageHandler;

    public UpdateHandler(
        ITelegramCallbackHandler callbackHandler,
        ITelegramMessageHandler telegramMessageHandler)
    {
        _callbackHandler = callbackHandler;
        _telegramMessageHandler = telegramMessageHandler;
    }

    public async Task Handle(Update update)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                await _telegramMessageHandler.Handle(update.ToMessage());
                return;
            case UpdateType.CallbackQuery:
                await _callbackHandler.Handle(update.ToCallback());
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(update.Type));
        }
    }
}