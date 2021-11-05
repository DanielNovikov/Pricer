using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Background.Jobs;

namespace PriceObserver.Background
{
    public static class DependencyInjection
    {
        public static void AddBackgroundJobs(this IServiceCollection services)
        {
            services.AddHostedService<ItemsObserverBackgroundService>();
        }
    }
}