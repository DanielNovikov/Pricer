using PriceObserver.Telegram.Abstract.Client;
using PriceObserver.Telegram.Abstract.Commands;
using PriceObserver.Telegram.Extensions;
using Telegram.Bot.Args;

namespace PriceObserver.Telegram.Concrete.Client
{
    public class TelegramBotProcessor : ITelegramBotProcessor
    {
        private readonly ITelegramBot _telegramBot;
        private readonly ITelegramBotService _telegramBotService;
        private readonly ICommandProcessor _commandProcessor;

        public TelegramBotProcessor(
            ITelegramBot telegramBot,
            ITelegramBotService telegramBotService,
            ICommandProcessor commandProcessor)
        {
            _telegramBot = telegramBot;
            _telegramBotService = telegramBotService;
            _commandProcessor = commandProcessor;
        }

        public void StartProcessing()
        {
            var client = _telegramBot.GetClient();

            client.OnUpdate += OnUpdate;
            client.StartReceiving();
        }

        private async void OnUpdate(object sender, UpdateEventArgs updateEventArgs)
        {
            var update = updateEventArgs.Update;
            var userId = update.GetUserId();

            var result = await _commandProcessor.Process(update);

            var message = result.IsSuccess ? result.Result : result.Error;
            
            if (!string.IsNullOrEmpty(message))
                await _telegramBotService.SendMessage(userId, message);
        }
    }
}