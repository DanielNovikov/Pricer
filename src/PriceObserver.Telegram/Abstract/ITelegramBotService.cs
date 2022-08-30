using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Abstract;

public interface ITelegramBotService
{
    Task SendMessage(long userId, string message);
        
    Task SendMessageWithReplyMarkup(long userId, string message, IReplyMarkup keyboard);

    Task SendVideo(long userId, string videoUrl, string message = default);
    
    Task EditMessage(long userId, int messageId, string message, InlineKeyboardMarkup keyboard);
    
    Task DeleteMessage(long userId, int messageId);
}