using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Background.Jobs;
using PriceObserver.Background.JobServices.Abstract;
using PriceObserver.Background.JobServices.Concrete;

namespace PriceObserver.Background;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        return services
            .AddHostedService<ItemsPriceObserver>()
            .AddHostedService<TelegramUpdateReceiverJob>()
            .AddHostedService<AppNotificationsSender>()
            
            .AddTransient<IItemsPriceObserverService, ItemsPriceObserverService>()
            .AddTransient<IItemPriceChanger, ItemPriceChanger>()
            .AddTransient<IItemRemovalService, ItemRemovalService>();
    }
}