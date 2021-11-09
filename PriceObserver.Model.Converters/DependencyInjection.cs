using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Converters.Concrete;

namespace PriceObserver.Model.Converters
{
    public static class DependencyInjection
    {
        public static void AddConverters(this IServiceCollection services)
        {
            services.AddTransient<IChatToUserConverter, ChatToUserConverter>();
            services.AddTransient<IItemToItemVMConverter, ItemToItemVMConverter>();
            services.AddTransient<IShopToShopWithItemsVMConverter, ShopToShopWithItemsVmConverter>();
        }
    }
}