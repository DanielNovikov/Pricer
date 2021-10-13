using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Telegram.Commands.All;

namespace PriceObserver.Model.Converters.Concrete
{
    public class ItemToItemDtoConverter : IItemToItemDtoConverter
    {
        public ItemDto Convert(Item source)
        {
            return new ItemDto
            {
                Identifier = $"Identifier: {source.Id}",
                Price = $"Price: {source.Price}",
                Url = source.Url.ToString()
            };
        }
    }
}