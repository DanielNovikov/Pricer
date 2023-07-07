using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Persistent.Repositories.Concrete;

namespace Pricer.Data.Persistent;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistentDataRepositories(this IServiceCollection services)
    {
        return services
            .AddMemoryCache()
            .AddScoped<IItemRepository, ItemRepository>()
            .AddScoped<IItemPriceChangeRepository, ItemPriceChangeRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserTokenRepository, UserTokenRepository>()
            .AddScoped<IItemParseResultRepository, ItemParseResultRepository>();
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<ApplicationDbContext>(
            context => context.UseNpgsql(
                configuration.GetConnectionString("PricerDB"),
                x => x.MigrationsAssembly("Pricer.Data.Persistent")));
    }
}