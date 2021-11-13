using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Telegram.Client.Abstract;
using PriceObserver.Telegram.Dialog.Input.Abstract;
using Telegram.Bot.Args;
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

            client.OnUpdate += OnUpdate;

            var allowedUpdateTypes = new[] { UpdateType.Message };
            client.StartReceiving(allowedUpdateTypes, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void OnUpdate(object sender, UpdateEventArgs updateEventArgs)
        {
            using var scope = _serviceProvider.CreateScope();

            var updateConverter = scope.ServiceProvider.GetService<IUpdateToUpdateDtoConverter>();
            var inputHandler = scope.ServiceProvider.GetService<IInputHandler>();
            var telegramBotService = scope.ServiceProvider.GetService<ITelegramBotService>();
            
            var update = updateEventArgs.Update;
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
    }
}