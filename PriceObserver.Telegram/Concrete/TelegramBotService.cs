using System.Threading.Tasks;
using PriceObserver.Telegram.Abstract;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Concrete
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly ITelegramBot _telegramBot;

        public TelegramBotService(ITelegramBot telegramBot)
        {
            _telegramBot = telegramBot;
        }
        
        public async Task SendMessage(long userId, string message)
        {
            await _telegramBot.GetClient().SendTextMessageAsync(userId, message, ParseMode.Html);
        }

        public async Task SendKeyboard(long userId, string message, ReplyKeyboardMarkup keyboard)
        {
            await _telegramBot.GetClient().SendTextMessageAsync(userId, message, replyMarkup: keyboard);
        }
    }
}