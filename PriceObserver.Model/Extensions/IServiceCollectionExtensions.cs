using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Converters.Concrete;

namespace PriceObserver.Model.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureConverters(this IServiceCollection services)
        {
            services.AddTransient<IUpdateToUserConverter, UpdateToUserConverter>();
            services.AddTransient<IItemToAllCommandItemDtoConverter, ItemToAllCommandItemDtoConverter>();
        }
    }
}