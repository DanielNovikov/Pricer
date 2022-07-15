using Microsoft.Extensions.Options;
using PriceObserver.Telegram.Abstract;
using PriceObserver.Telegram.Options;
using Telegram.Bot;

namespace PriceObserver.Telegram.Concrete;

public class TelegramBot : ITelegramBot
{
    public TelegramBot(IOptions<TelegramClientOptions> telegramClientOptions)
    {
        var accessToken = telegramClientOptions.Value.AccessToken;
        Client = new TelegramBotClient(accessToken);
    }
    
    public ITelegramBotClient Client { get; }
}