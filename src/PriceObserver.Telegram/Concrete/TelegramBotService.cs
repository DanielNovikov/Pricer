using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Telegram.Abstract;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Concrete;

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

    public async Task SendMessageWithKeyboard(long userId, string message, ReplyKeyboardMarkup keyboard)
    {
        await Send(userId, message, keyboard);
    }

    private const int UserDeactivatedErrorCode = 403;

    private int _retryAttempt;
    private const int RetrySendMessageCount = 3;
    private const int RetrySendMessagePause = 1000;
    private const int MaximumMessageLengthInError = 120;
        
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
                if (_retryAttempt == RetrySendMessageCount)
                {
                    _logger.LogError("Maximum count of attempts exhausted");
                    return;
                }

                _logger.LogError(
                    "Send error. User id: {0}, Message: {1}, Exception message: {2}",
                    userId, 
                    message[..MaximumMessageLengthInError],
                    ex.Message);

                _retryAttempt++;
                await Task.Delay(RetrySendMessagePause);
                
                await Send(userId, message, keyboard);
                    
                return;
            }

            _logger.LogInformation("User deactivated with id {0}", userId);
            await _userService.DeactivateUserById(userId);
        }
    }
}