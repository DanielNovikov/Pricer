using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Abstract;

public interface ITelegramBotService
{
    Task SendMessage(long userId, string message);
        
    Task SendMessageWithKeyboard(long userId, string message, ReplyKeyboardMarkup keyboard);
}