using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Repositories.Concrete;

namespace PriceObserver.Data
{
    public static class DependencyInjection
    {
        public static void AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                context => context.UseNpgsql(
                    configuration.GetConnectionString("PricerDB"),
                    x => x.MigrationsAssembly("PriceObserver.Data")));

            services.AddTransient<ICommandRepository, CommandRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IMenuCommandRepository, MenuCommandRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IShopRepository, ShopRepository>();
        }
    }
}