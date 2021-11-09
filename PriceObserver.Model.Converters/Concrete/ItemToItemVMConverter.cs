using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Service;

namespace PriceObserver.Model.Converters.Concrete
{
    public class ItemToItemVMConverter : IItemToItemVMConverter
    {
        public ItemVM Convert(Item source)
        {
            return new ItemVM
            {
                Id = source.Id,
                Price = source.Price,
                Title = source.Title,
                Url = source.Url,
                ImageUrl = source.ImageUrl
            };
        }
    }
}