using Telegram.Bot;

namespace PriceObserver.Telegram.Abstract.Client
{
    public interface ITelegramBot
    {
        TelegramBotClient GetClient();
    }
}