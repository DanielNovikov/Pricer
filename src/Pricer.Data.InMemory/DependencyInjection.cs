using Microsoft.Extensions.DependencyInjection;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.InMemory.Repositories.Concrete;

namespace Pricer.Data.InMemory;

public static class DependencyInjection
{
    public static IServiceCollection AddInMemoryDataRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IResourceRepository, ResourceRepository>()
            .AddScoped<ICommandRepository, CommandRepository>()
            .AddScoped<IMenuRepository, MenuRepository>()
            .AddScoped<IShopRepository, ShopRepository>()
            .AddScoped<IShopCategoryRepository, ShopCategoryRepository>()
            .AddScoped<ICurrencyRepository, CurrencyRepository>();
    }
}