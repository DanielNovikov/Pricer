using PriceObserver.Model.Telegram.Input;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Model.Converters.Abstract
{
    public interface IUpdateDtoToUserConverter : IConverter<UpdateDto, User>
    {
    }
}