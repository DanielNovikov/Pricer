using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Telegram.Abstract;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Concrete
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly ITelegramBot _telegramBot;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        
        public TelegramBotService(
            ITelegramBot telegramBot,
            IUserService userService,
            ILogger<TelegramBotService> logger)
        {
            _telegramBot = telegramBot;
            _userService = userService;
            _logger = logger;
        }
        
        public async Task SendMessage(long userId, string message)
        {
            try
            {
                await _telegramBot.GetClient().SendTextMessageAsync(userId, message, ParseMode.Html);
            }
            catch
            {
                await DeactivateUser(userId);
            }
        }

        public async Task SendKeyboard(long userId, string message, ReplyKeyboardMarkup keyboard)
        {
            try
            {
                await _telegramBot.GetClient().SendTextMessageAsync(userId, message, replyMarkup: keyboard);
            }
            catch
            {
                await DeactivateUser(userId);
            }
        }

        private async Task DeactivateUser(long userId)
        {
            _logger.LogWarning($"User deactivated with id {userId}");
            
            await _userService.DeactivateUserById(userId);
        }
    }
}