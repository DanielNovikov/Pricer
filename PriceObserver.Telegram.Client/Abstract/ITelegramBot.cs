using Telegram.Bot;

namespace PriceObserver.Telegram.Client.Abstract
{
    public interface ITelegramBot
    {
        TelegramBotClient GetClient();
    }
}