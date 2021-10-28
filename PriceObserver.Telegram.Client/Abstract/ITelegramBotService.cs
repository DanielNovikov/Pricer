using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Client.Abstract
{
    public interface ITelegramBotService
    {
        Task SendMessage(long userId, string message);
        
        Task SendKeyboard(long userId, string message, ReplyKeyboardMarkup keyboard);
    }
}