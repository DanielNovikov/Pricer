using PriceObserver.Dialog.Input.Models;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Extensions
{
    public static class UpdateExtensions
    {
        public static UpdateDto ToDto(this Update update)
        {
            var message = update.Message;
            var chat = message.Chat;
            
            return new UpdateDto(
                message.Text,
                chat.Id,
                chat.Username,
                chat.FirstName,
                chat.LastName);
        }
    }
}