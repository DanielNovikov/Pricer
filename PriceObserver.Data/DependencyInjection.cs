using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Repositories.Concrete;

namespace PriceObserver.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddMemoryCache();
            
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IItemPriceChangeRepository, ItemPriceChangeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserTokenRepository, UserTokenRepository>();
        services.AddScoped<IAppNotificationRepository, AppNotificationRepository>();
        services.AddScoped<IItemParseResultRepository, ItemParseResultRepository>();

        return services;
    }

    public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            context => context.UseNpgsql(
                configuration.GetConnectionString("PricerDB"),
                x => x.MigrationsAssembly("PriceObserver.Data")));

        return services;
    }
}