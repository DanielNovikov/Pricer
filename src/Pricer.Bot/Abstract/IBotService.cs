using Pricer.Data.Persistent.Models.Enums;
using Pricer.Dialog.Models;

namespace Pricer.Bot.Abstract;

public interface IBotService
{
    Task SendText(BotKey key, string userId, string text);

    Task SendTextWithMenuKeyboard(BotKey key, string userId, string text, MenuKeyboard keyboard);
    
    Task SendTextWithMessageKeyboard(BotKey key, string userId, string text, MessageKeyboard keyboard);
    
    Task EditMessage(BotKey key, string userId, int messageId, string text);
    
    Task EditMessageWithKeyboard(BotKey key, string userId, int messageId, string text, MessageKeyboard keyboard);
    
    Task DeleteMessage(BotKey key, string userId, int messageId);
}