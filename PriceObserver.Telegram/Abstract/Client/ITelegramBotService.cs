using System.Threading.Tasks;

namespace PriceObserver.Telegram.Abstract.Client
{
    public interface ITelegramBotService
    {
        Task SendMessage(long userId, string message);
    }
}