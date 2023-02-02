using Microsoft.Extensions.Options;
using Pricer.Bot.Telegram.Abstract;
using Pricer.Bot.Telegram.Options;
using Telegram.Bot;

namespace Pricer.Bot.Telegram.Concrete;

public class TelegramBot : ITelegramBot
{
    public TelegramBot(IOptions<TelegramClientOptions> telegramClientOptions)
    {
        var accessToken = telegramClientOptions.Value.AccessToken;
        Client = new TelegramBotClient(accessToken);
    }
    
    public ITelegramBotClient Client { get; }
}