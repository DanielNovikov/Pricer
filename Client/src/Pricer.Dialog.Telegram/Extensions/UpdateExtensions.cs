using Pricer.Dialog.Callback.Models;
using Pricer.Dialog.Input.Models;
using Telegram.Bot.Types;

namespace Pricer.Dialog.Telegram.Extensions;

public static class UpdateExtensions
{
    public static MessageHandlingModel ToMessage(this Update update)
    {
        var message = update.Message ?? 
            throw new ArgumentNullException($"{nameof(ToMessage)}:{nameof(update.Message)}");
        
        var user = message.Chat.ToUser();
            
        return new MessageHandlingModel(
            message.Text!,
            user);
    }
    
    public static CallbackHandlingModel ToCallback(this Update update)
    {
        var callback = update.CallbackQuery ?? 
            throw new ArgumentNullException($"{nameof(ToCallback)}:{nameof(update.CallbackQuery)}");
        
        var message = callback.Message ??
            throw new ArgumentNullException($"{nameof(ToCallback)}:{nameof(update.Message)}");

        var user = message.Chat.ToUser();
        
        return new CallbackHandlingModel(
            callback.Data!,
            message.MessageId,
            user);
    }
}