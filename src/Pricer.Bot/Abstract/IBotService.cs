using Pricer.Bot.Models.Enums;
using Pricer.Dialog.Models;

namespace Pricer.Bot.Abstract;

public interface IBotService
{
    BotKey Key { get; }
    
    Task SendText(string userId, string text);

    Task SendTextWithMenuKeyboard(string userId, string text, MenuKeyboard keyboard);
    
    Task SendTextWithMessageKeyboard(string userId, string text, MessageKeyboard keyboard);
    
    Task EditMessage(string userId, int messageId, string text);
    
    Task EditMessageWithKeyboard(string userId, int messageId, string text, MessageKeyboard keyboard);
    
    Task DeleteMessage(string userId, int messageId);
}