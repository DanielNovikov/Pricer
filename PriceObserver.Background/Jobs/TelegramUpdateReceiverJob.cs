using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Telegram.Client.Abstract;
using PriceObserver.Telegram.Dialog.Input.Abstract;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var client = _telegramBot.GetClient();

            var allowedUpdateTypes = new[] { UpdateType.Message };

            var updateReceiver = new QueuedUpdateReceiver(client);

            updateReceiver.StartReceiving(
                allowedUpdateTypes,
                HandleError,
                cancellationToken);
            
            await foreach (var update in updateReceiver.YieldUpdatesAsync().WithCancellation(cancellationToken))
            {
                await HandleUpdate(update);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task HandleUpdate(Update update)
        {
            using var scope = _serviceProvider.CreateScope();

            var updateConverter = scope.ServiceProvider.GetService<IUpdateToUpdateDtoConverter>();
            var inputHandler = scope.ServiceProvider.GetService<IInputHandler>();
            var telegramBotService = scope.ServiceProvider.GetService<ITelegramBotService>();
            
            var updateDto = updateConverter!.Convert(update);
            var userId = updateDto.UserId;

            var result = await inputHandler!.Handle(updateDto);

            if (!result.IsSuccess)
            {
                await telegramBotService!.SendMessage(userId, result.Error);
                return;
            }

            var hasKeyboard = result.Result.MenuKeyboard != null;
            if (hasKeyboard)
            {
                await telegramBotService!.SendKeyboard(userId, result.Result.Message, result.Result.MenuKeyboard);
                return;
            }

            await telegramBotService!.SendMessage(userId, result.Result.Message);
        }
        
        private Task HandleError(
            Exception exception,
            CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var logger = scope.ServiceProvider.GetService<ILogger<TelegramUpdateReceiverJob>>();
            
            logger.LogError($@"Message: {exception.Message}
InnerException: {exception.InnerException}");
            
            return Task.CompletedTask;
        }
    }
}