using PriceObserver.Model.Converters.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Model.Converters.Concrete
{
    public class UpdateToUserConverter : IUpdateToUserConverter
    {
        public User Convert(Update source)
        {
            var chat = source.Message.Chat;

            return new User
            {
                Id = chat.Id,
                Username = chat.Username,
                FirstName = chat.FirstName,
                LastName = chat.LastName
            };
        }
    }
}