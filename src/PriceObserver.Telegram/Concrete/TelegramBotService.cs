using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Telegram.Abstract;
using System;
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
        await Send(userId, message,
            async () => await _telegramBot.Client.SendTextMessageAsync(userId, message, Formatting));
    }

    public async Task SendMessageWithKeyboard(long userId, string message, ReplyKeyboardMarkup keyboard)
    {
        await Send(userId, message,
            async () => await _telegramBot.Client.SendTextMessageAsync(userId, message, Formatting, replyMarkup: keyboard));
    }

    public async Task SendVideo(long userId, string videoUrl, string message = default)
    {
        await Send(userId, message, 
            async () => await _telegramBot.Client.SendVideoAsync(userId, videoUrl, caption: message, parseMode: Formatting));
    }

    private const ParseMode Formatting = ParseMode.Html;
    
    private const int UserDeactivatedErrorCode = 403;

    private int _retryAttempt;
    private const int RetrySendMessageCount = 3;
    private const int RetrySendMessagePause = 1000;
    private const int MaximumMessageLengthInError = 120;
    
    private async Task Send(long userId, string message, Func<Task> action)
    {
        try
        {
            await action();
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
                
                await Send(userId, message, action);
                    
                return;
            }

            _logger.LogInformation("User deactivated with id {0}", userId);
            await _userService.DeactivateUserById(userId);
        }
    }
}