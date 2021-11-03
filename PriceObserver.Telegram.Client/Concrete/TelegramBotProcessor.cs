using System;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Telegram.Client.Abstract;
using PriceObserver.Telegram.Dialog.Common.Extensions;
using PriceObserver.Telegram.Dialog.Input.Abstract;
using Telegram.Bot.Args;

namespace PriceObserver.Telegram.Client.Concrete
{
    public class TelegramBotProcessor : ITelegramBotProcessor
    {
        private readonly ITelegramBot _telegramBot;
        private readonly IServiceProvider _serviceProvider;

        public TelegramBotProcessor(
            ITelegramBot telegramBot,
            IServiceProvider serviceProvider)
        {
            _telegramBot = telegramBot;
            _serviceProvider = serviceProvider;
        }

        public void StartProcessing()
        {
            var client = _telegramBot.GetClient();

            client.OnUpdate += OnUpdate;
            client.StartReceiving();
        }

        private async void OnUpdate(object sender, UpdateEventArgs updateEventArgs)
        {
            using var scope = _serviceProvider.CreateScope();

            var inputHandler = scope.ServiceProvider.GetService<IInputHandler>();
            var telegramBotService = scope.ServiceProvider.GetService<ITelegramBotService>();
            
            var update = updateEventArgs.Update;
            var userId = update.GetUserId();

            var result = await inputHandler.Handle(update);

            if (!result.IsSuccess)
            {
                await telegramBotService.SendMessage(userId, result.Error);
                return;
            }

            var hasKeyboard = result.Result.MenuKeyboard != null;
            if (hasKeyboard)
            {
                await telegramBotService.SendKeyboard(userId, result.Result.Message, result.Result.MenuKeyboard);
                return;
            }

            await telegramBotService.SendMessage(userId, result.Result.Message);
        }
    }
}