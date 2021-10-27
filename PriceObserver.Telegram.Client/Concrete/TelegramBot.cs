using Microsoft.Extensions.Options;
using PriceObserver.Model.Telegram.Options;
using PriceObserver.Telegram.Client.Abstract;
using Telegram.Bot;

namespace PriceObserver.Telegram.Client.Concrete
{
    public class TelegramBot : ITelegramBot
    {
        private TelegramBotClient _client;

        public TelegramBot(IOptions<TelegramClientOptions> telegramClientOptions)
        {
            InitializeClient(telegramClientOptions);
        }

        public TelegramBotClient GetClient()
        {
            return _client;
        }

        private void InitializeClient(IOptions<TelegramClientOptions> telegramClientOptions)
        {
            var accessToken = telegramClientOptions.Value.AccessToken;
            _client = new TelegramBotClient(accessToken);
        }
    }
}