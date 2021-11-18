using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Converters.Concrete;

namespace PriceObserver.Model.Converters
{
    public static class DependencyInjection
    {
        public static void AddConverters(this IServiceCollection services)
        {
            services.AddTransient<IUpdateDtoToUserConverter, UpdateDtoToUserConverter>();
            services.AddTransient<IItemToItemVMConverter, ItemToItemVMConverter>();
            services.AddTransient<IShopToShopVMConverter, ShopToShopVMConverter>();
            services.AddTransient<IUpdateToUpdateDtoConverter, UpdateToUpdateDtoConverter>();
        }
    }
}