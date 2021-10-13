using PriceObserver.Model.Converters.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Model.Converters.Concrete
{
    public class UpdateToUserConverter : IUpdateToUserConverter
    {
        public User Convert(Chat source)
        {
            return new User
            {
                Id = source.Id,
                Username = source.Username,
                FirstName = source.FirstName,
                LastName = source.LastName
            };
        }
    }
}