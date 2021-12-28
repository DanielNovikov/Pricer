using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Background.Jobs;
using PriceObserver.Background.JobServices.Abstract;
using PriceObserver.Background.JobServices.Concrete;

namespace PriceObserver.Background
{
    public static class DependencyInjection
    {
        public static void AddBackgroundJobs(this IServiceCollection services)
        {
            services.AddHostedService<ItemsPriceObserver>();
            services.AddHostedService<TelegramUpdateReceiverJob>();
            services.AddHostedService<AppNotificationsSender>();

            services.AddTransient<IItemsPriceService, ItemsPriceService>();
        }
    }
}