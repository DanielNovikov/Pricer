using User = PriceObserver.Data.Models.User;

namespace PriceObserver.Dialog.Input.Models
{
    public class UpdateDto
    {
        public string Text { get; set; }
        
        public long UserId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Username { get; set; }
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