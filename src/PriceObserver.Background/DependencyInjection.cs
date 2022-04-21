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
            .AddHostedService<ItemsObserver>()
            .AddHostedService<TelegramMessageReceiver>()
            .AddHostedService<AppNotificationsSender>()
            
            .AddTransient<IItemsObserverService, ItemsObserverService>()
            .AddTransient<IItemPriceChanger, ItemPriceChanger>()
            .AddTransient<IItemRemovalService, ItemRemovalService>();
    }
}