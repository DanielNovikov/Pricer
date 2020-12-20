using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Repositories.Concrete;

namespace PriceObserver.Data.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void ConfigureData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ObserverContext>(
                context => context.UseSqlite(configuration.GetConnectionString("ObserverDatabase")),
                ServiceLifetime.Singleton);

            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}