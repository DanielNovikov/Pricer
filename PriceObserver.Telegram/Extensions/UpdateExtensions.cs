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
            
            return new UpdateDto
            {
                Text = message.Text,
                UserId = chat.Id,
                Username = chat.Username,
                FirstName = chat.FirstName,
                LastName = chat.LastName
            };
        }
    }
}