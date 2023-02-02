using Telegram.Bot;

namespace Pricer.Bot.Telegram.Abstract;

public interface ITelegramBot
{
    ITelegramBotClient Client { get; }
}