using Telegram.Bot;

namespace Pricer.Telegram.Abstract;

public interface ITelegramBot
{
    ITelegramBotClient Client { get; }
}