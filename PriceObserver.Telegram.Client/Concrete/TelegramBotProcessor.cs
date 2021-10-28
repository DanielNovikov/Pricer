using PriceObserver.Telegram.Client.Abstract;
using PriceObserver.Telegram.Dialog.Common.Extensions;
using PriceObserver.Telegram.Dialog.Input.Abstract;
using Telegram.Bot.Args;

namespace PriceObserver.Telegram.Client.Concrete
{
    public class TelegramBotProcessor : ITelegramBotProcessor
    {
        private readonly ITelegramBot _telegramBot;
        private readonly ITelegramBotService _telegramBotService;
        private readonly IInputHandler _inputHandler;

        public TelegramBotProcessor(
            ITelegramBot telegramBot,
            ITelegramBotService telegramBotService,
            IInputHandler inputHandler)
        {
            _telegramBot = telegramBot;
            _telegramBotService = telegramBotService;
            _inputHandler = inputHandler;
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

            var result = await _inputHandler.Handle(update);

            if (!result.IsSuccess)
            {
                await _telegramBotService.SendMessage(userId, result.Error);
                return;
            }

            var hasKeyboard = result.Result.MenuKeyboard != null;
            if (hasKeyboard)
            {
                await _telegramBotService.SendKeyboard(userId, result.Result.Message, result.Result.MenuKeyboard);
                return;
            }

            await _telegramBotService.SendMessage(userId, result.Result.Message);
        }
    }
}