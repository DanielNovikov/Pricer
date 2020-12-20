using System.Threading.Tasks;
using PriceObserver.Telegram.Abstract.Client;

namespace PriceObserver.Telegram.Concrete.Client
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