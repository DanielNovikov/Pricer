using PriceObserver.Dialog.Services.Models;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Extensions;

public static class UpdateExtensions
{
    public static UpdateServiceModel ToDto(this Update update)
    {
        var message = update.Message;
        var chat = message.Chat;
            
        return new UpdateServiceModel(
            message.Text,
            chat.Id,
            chat.Username,
            chat.FirstName,
            chat.LastName);
    }
}