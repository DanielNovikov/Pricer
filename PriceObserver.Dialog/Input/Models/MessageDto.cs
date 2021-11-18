using PriceObserver.Data.Models;

namespace PriceObserver.Dialog.Input.Models
{
    public class MessageDto
    {
        public MessageDto(
            string text,
            User user)
        {
            Text = text;
            User = user;
        }

        public string Text { get; }
        
        public User User { get; }
    }
}