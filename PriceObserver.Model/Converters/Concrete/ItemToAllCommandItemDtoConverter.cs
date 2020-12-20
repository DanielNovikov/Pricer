using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Telegram.Commands.All;

namespace PriceObserver.Model.Converters.Concrete
{
    public class ItemToAllCommandItemDtoConverter : IItemToAllCommandItemDtoConverter
    {
        public AllCommandItemDto Convert(Item source)
        {
            return new AllCommandItemDto
            {
                Identifier = $"Identifier: {source.Id}",
                Price = $"Price: {source.Price}",
                Url = source.Url.ToString()
            };
        }
    }
}