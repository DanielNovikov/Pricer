using Microsoft.Extensions.Options;
using Pricer.Telegram.Abstract;
using Pricer.Telegram.Options;
using Telegram.Bot;

namespace Pricer.Telegram.Concrete;

public class TelegramBot : ITelegramBot
{
    public TelegramBot(IOptions<TelegramClientOptions> telegramClientOptions)
    {
        var accessToken = telegramClientOptions.Value.AccessToken;
        Client = new TelegramBotClient(accessToken);
    }
    
    public ITelegramBotClient Client { get; }
}