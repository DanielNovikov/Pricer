using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Service;

namespace PriceObserver.Model.Converters.Concrete
{
    public class ShopToShopVMConverter : IShopToShopVMConverter
    {
        public ShopVM Convert(Shop source)
        {
            return new ShopVM
            {
                Address = $"https://{source.Host}",
                LogoUrl = source.LogoUrl.ToString()
            };
        }
    }
}