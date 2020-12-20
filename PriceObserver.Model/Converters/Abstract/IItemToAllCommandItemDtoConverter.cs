using PriceObserver.Model.Data;
using PriceObserver.Model.Telegram.Commands.All;

namespace PriceObserver.Model.Converters.Abstract
{
    public interface IItemToAllCommandItemDtoConverter : IConverter<Item, AllCommandItemDto>
    {
        
    }
}