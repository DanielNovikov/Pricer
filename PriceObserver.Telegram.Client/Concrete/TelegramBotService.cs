using System.Threading.Tasks;
using PriceObserver.Telegram.Client.Abstract;

namespace PriceObserver.Telegram.Client.Concrete
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
            await _telegramBot.GetClient().SendTextMessageAsync(userId, message);
        }
    }
}