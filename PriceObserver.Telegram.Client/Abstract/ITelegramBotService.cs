using System.Threading.Tasks;

namespace PriceObserver.Telegram.Client.Abstract
{
    public interface ITelegramBotService
    {
        Task SendMessage(long userId, string message);
    }
}