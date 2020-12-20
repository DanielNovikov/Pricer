using Microsoft.Extensions.DependencyInjection;

namespace PriceObserver.Jobs.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void ConfigureBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<ItemsObserverBackgroundService>();
        }
    }
}