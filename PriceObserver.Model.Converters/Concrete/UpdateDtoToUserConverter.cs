using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Dialog.Input;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Model.Converters.Concrete
{
    public class UpdateDtoToUserConverter : IUpdateDtoToUserConverter
    {
        public User Convert(UpdateDto source)
        {
            return new User
            {
                Id = source.UserId,
                Username = source.Username,
                FirstName = source.FirstName,
                LastName = source.LastName
            };
        }
    }
}