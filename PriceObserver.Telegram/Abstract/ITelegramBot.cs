using Telegram.Bot;

namespace PriceObserver.Telegram.Abstract
{
    public interface ITelegramBot
    {
        ITelegramBotClient GetClient();
    }
}