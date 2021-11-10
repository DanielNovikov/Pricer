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
                Address = $"https://{source.Host}",
                LogoUrl = source.LogoUrl.ToString()
            };
        }
    }
}