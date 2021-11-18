using PriceObserver.Model.Dialog.Input;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Model.Converters.Abstract
{
    public interface IUpdateDtoToUserConverter : IConverter<UpdateDto, User>
    {
    }
}