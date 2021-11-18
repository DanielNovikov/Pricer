using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Model.Dialog.Input
{
    public class UpdateDto
    {
        public string Text { get; set; }
        
        public long UserId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Username { get; set; }
    }

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
    
    public static class UpdateDtoExtensions
    {
        public static User ToUser(this UpdateDto update)
        {
            return new User
            {
                Id = update.UserId,
                Username = update.Username,
                FirstName = update.FirstName,
                LastName = update.LastName
            };
        }
    }
}