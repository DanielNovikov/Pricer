using Microsoft.Extensions.Options;
using PriceObserver.Telegram.Abstract;
using PriceObserver.Telegram.Options;
using Telegram.Bot;

namespace PriceObserver.Telegram.Concrete
{
    public class TelegramBot : ITelegramBot
    {
        private TelegramBotClient _client;

        public TelegramBot(IOptions<TelegramClientOptions> telegramClientOptions)
        {
            InitializeClient(telegramClientOptions);
        }

        public ITelegramBotClient GetClient()
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