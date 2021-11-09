using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Service;

namespace PriceObserver.Model.Converters.Concrete
{
    public class ShopToShopWithItemsVmConverter : IShopToShopWithItemsVMConverter
    {
        public ShopWithItemsVM Convert(Shop source)
        {
            return new ShopWithItemsVM
            {
                Title = source.Name,
                Host = source.Host
            };
        }
    }
}