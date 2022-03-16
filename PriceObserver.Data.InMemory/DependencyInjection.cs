using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.InMemory.Repositories.Concrete;

namespace PriceObserver.Data.InMemory;

public static class DependencyInjection
{
    public static IServiceCollection AddInMemoryData(this IServiceCollection services)
    {
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<ICommandRepository, CommandRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<IShopRepository, ShopRepository>();

        return services;
    }
}