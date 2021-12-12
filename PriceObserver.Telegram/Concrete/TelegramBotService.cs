using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Telegram.Abstract;
using Telegram.Bot.Exceptions;
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
            await Send(userId, message);
        }

        public async Task SendKeyboard(long userId, string message, ReplyKeyboardMarkup keyboard)
        {
            await Send(userId, message, keyboard);
        }

        private const int UserDeactivatedErrorCode = 403;
        private const int SendMessagePause = 1000;
        
        private async Task Send(long userId, string message, IReplyMarkup keyboard = default)
        {
            try
            {
                await _telegramBot.GetClient()
                    .SendTextMessageAsync(userId, message, ParseMode.Html, replyMarkup: keyboard);
            }
            catch (ApiRequestException ex)
            {
                if (ex.ErrorCode != UserDeactivatedErrorCode)
                {
                    _logger.LogWarning($"Send error. Message: {ex.Message}");

                    await Task.Delay(SendMessagePause);
                    await Send(userId, message, keyboard);
                    
                    return;
                }

                _logger.LogWarning($"User deactivated with id {userId}");
                await _userService.DeactivateUserById(userId);
            }
        }
    }
}