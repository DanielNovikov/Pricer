using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PriceObserver.Common.Extensions;
using PriceObserver.Telegram.Abstract;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using IUpdateHandler = PriceObserver.Telegram.Abstract.IUpdateHandler;

namespace PriceObserver.Background.Jobs;

public class TelegramMessageReceiver : IHostedService
{
    private readonly ITelegramBot _telegramBot;
    private readonly IServiceProvider _serviceProvider;

    public TelegramMessageReceiver(
        ITelegramBot telegramBot,
        IServiceProvider serviceProvider)
    {
        _telegramBot = telegramBot;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var client = _telegramBot.GetClient();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[] {UpdateType.Message}
            };

            client.StartReceiving(
                HandleUpdateAsync,
                HandleError,
                receiverOptions,
                cancellationToken);
        }
        catch (Exception ex)
        {
            using var scope = _serviceProvider.CreateScope();
            var logger = scope.GetService<ILogger<TelegramMessageReceiver>>();
            
            logger.LogError("Creation of telegram bot failed with exception of type {1}", ex.GetType().FullName);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var updateHandler = scope.GetService<IUpdateHandler>();

        await updateHandler.Handle(update);
    }
        
    private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var logger = scope.GetService<ILogger<TelegramMessageReceiver>>();

        logger.LogError($@"Receive error
Exception type: {exception.GetType().FullName}
Message: {exception.Message}
Inner exception: {exception.InnerException}");

        return Task.CompletedTask;
    }
}