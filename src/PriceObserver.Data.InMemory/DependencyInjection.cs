using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.InMemory.Repositories.Concrete;

namespace PriceObserver.Data.InMemory;

public static class DependencyInjection
{
    public static IServiceCollection AddInMemoryDataRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IResourceRepository, ResourceRepository>()
            .AddScoped<ICommandRepository, CommandRepository>()
            .AddScoped<IMenuRepository, MenuRepository>()
            .AddScoped<IShopRepository, ShopRepository>();
    }
}