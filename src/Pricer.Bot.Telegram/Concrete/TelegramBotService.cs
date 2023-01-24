using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pricer.Bot.Models.Enums;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Models;
using Pricer.Telegram.Abstract;
using Pricer.Telegram.Extensions;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;

namespace Pricer.Telegram.Concrete;

public class TelegramBotService : ITelegramBotService
{
    private readonly ITelegramBot _telegramBot;
    private readonly IUserService _userService;
    private readonly ILogger _logger;
    private readonly IReplyMarkupBuilder _replyMarkupBuilder;
    private readonly IInlineKeyboardMarkupBuilder _inlineKeyboardMarkupBuilder;
        
    public TelegramBotService(
        ITelegramBot telegramBot,
        IUserService userService,
        ILogger<TelegramBotService> logger, 
        IReplyMarkupBuilder replyMarkupBuilder, 
        IInlineKeyboardMarkupBuilder inlineKeyboardMarkupBuilder)
    {
        _telegramBot = telegramBot;
        _userService = userService;
        _logger = logger;
        _replyMarkupBuilder = replyMarkupBuilder;
        _inlineKeyboardMarkupBuilder = inlineKeyboardMarkupBuilder;
    }

    public BotKey Key => BotKey.Telegram;
    
    public async Task SendText(string userId, string text)
    {
        await Send(userId, text,
            async () => await _telegramBot.Client.SendTextMessageAsync(userId.ToLong(), text, ParseMode.Html));
    }

    public async Task SendTextWithMenuKeyboard(string userId, string text, MenuKeyboard keyboard)
    {
        var replyMarkup = _replyMarkupBuilder.Build(keyboard);
        
        await Send(userId, text,
            async () => await _telegramBot.Client.SendTextMessageAsync(userId.ToLong(), text, ParseMode.Html, replyMarkup: replyMarkup));
    }

    public async Task SendTextWithMessageKeyboard(string userId, string text, MessageKeyboard keyboard)
    {
        var inlineKeyboardMarkup = _inlineKeyboardMarkupBuilder.Build(keyboard);
        
        await Send(userId, text,
            async () => await _telegramBot.Client.SendTextMessageAsync(userId.ToLong(), text, ParseMode.Html, replyMarkup: inlineKeyboardMarkup));
    }

    public async Task EditMessage(string userId, int messageId, string text)
    {
        await Send(userId, text, 
            async () => await _telegramBot.Client.EditMessageTextAsync(userId.ToLong(), messageId, text, parseMode: ParseMode.Html));
    }

    public async Task EditMessageWithKeyboard(string userId, int messageId, string text, MessageKeyboard keyboard)
    {
        var inlineKeyboardMarkup = _inlineKeyboardMarkupBuilder.Build(keyboard);
        
        await Send(userId, text, 
            async () => await _telegramBot.Client.EditMessageTextAsync(userId.ToLong(), messageId, text, replyMarkup: inlineKeyboardMarkup, parseMode: ParseMode.Html));
    }

    public async Task DeleteMessage(string userId, int messageId)
    {
        await Send(userId, default, 
            async () => await _telegramBot.Client.DeleteMessageAsync(userId.ToLong(), messageId));
    }
    
    private const int UserDeactivatedErrorCode = 403;

    private int _retryAttempt;
    private const int RetrySendMessageCount = 3;
    private const int RetrySendMessagePause = 1000;
    private const int MaximumMessageLengthInError = 120;
    
    private async Task Send(string userId, string message, Func<Task> action)
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