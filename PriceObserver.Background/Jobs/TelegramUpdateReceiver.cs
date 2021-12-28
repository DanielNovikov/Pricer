using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PriceObserver.Telegram.Abstract;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using IUpdateHandler = PriceObserver.Telegram.Abstract.IUpdateHandler;

namespace PriceObserver.Background.Jobs
{
    public class TelegramUpdateReceiverJob : IHostedService
    {
        private readonly ITelegramBot _telegramBot;
        private readonly IServiceProvider _serviceProvider;

        public TelegramUpdateReceiverJob(
            ITelegramBot telegramBot,
            IServiceProvider serviceProvider)
        {
            _telegramBot = telegramBot;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var client = _telegramBot.GetClient();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[] { UpdateType.Message } 
            };
            
            client.StartReceiving(
                HandleUpdateAsync, 
                HandleError, 
                receiverOptions, 
                cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var updateHandler = scope.ServiceProvider.GetService<IUpdateHandler>();

            await updateHandler!.Handle(update);
        }
        
        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var logger = scope.ServiceProvider.GetService<ILogger<TelegramUpdateReceiverJob>>();

            logger.LogError($@"Receive error
Message: {exception.Message}
InnerException: {exception.InnerException}");

            return Task.CompletedTask;
        }
    }
}