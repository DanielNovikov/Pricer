using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Repositories.Concrete;

namespace PriceObserver.Data;

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
            .AddScoped<IAppNotificationRepository, AppNotificationRepository>()
            .AddScoped<IItemParseResultRepository, ItemParseResultRepository>();
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<ApplicationDbContext>(
            context => context.UseNpgsql(
                configuration.GetConnectionString("PricerDB"),
                x => x.MigrationsAssembly("PriceObserver.Data")));
    }
}