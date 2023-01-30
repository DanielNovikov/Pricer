using Pricer.Bot.Abstract;
using Pricer.Data.Persistent.Models.Enums;
using Pricer.Dialog.Models;

namespace Pricer.Bot.Concrete;

public class BotService : IBotService
{
    private readonly IEnumerable<IBotProviderService> _botProviderServices;

    public BotService(IEnumerable<IBotProviderService> botProviderServices)
    {
        _botProviderServices = botProviderServices;
    }

    public async Task SendText(BotKey key, string userId, string text)
    {
        await GetBotProviderService(key).SendText(userId, text);
    }

    public async Task SendTextWithMenuKeyboard(BotKey key, string userId, string text, MenuKeyboard keyboard)
    {
        await GetBotProviderService(key).SendTextWithMenuKeyboard(userId, text, keyboard);
    }

    public async Task SendTextWithMessageKeyboard(BotKey key, string userId, string text, MessageKeyboard keyboard)
    {
        await GetBotProviderService(key).SendTextWithMessageKeyboard(userId, text, keyboard);
    }

    public async Task EditMessage(BotKey key, string userId, int messageId, string text)
    {
        await GetBotProviderService(key).EditMessage(userId, messageId, text);
    }

    public async Task EditMessageWithKeyboard(BotKey key, string userId, int messageId, string text, MessageKeyboard keyboard)
    {
        await GetBotProviderService(key).EditMessageWithKeyboard(userId, messageId, text, keyboard);
    }

    public async Task DeleteMessage(BotKey key, string userId, int messageId)
    {
        await GetBotProviderService(key).DeleteMessage(userId, messageId);
    }

    private IBotProviderService GetBotProviderService(BotKey key)
    {
        return _botProviderServices.First(x => x.Key == key);
    }
}