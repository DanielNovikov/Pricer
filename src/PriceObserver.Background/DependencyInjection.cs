using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Background.Jobs;
using PriceObserver.Background.Services.Abstract;
using PriceObserver.Background.Services.Concrete;

namespace PriceObserver.Background;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        return services
            .AddHostedService<ItemsObserver>()
            .AddHostedService<TelegramMessageReceiver>()
            .AddHostedService<AppNotificationsSender>()
            
            .AddTransient<IAppNotificationService, AppNotificationService>()
            .AddTransient<IItemsObserverService, ItemsObserverService>()
            .AddTransient<IItemPriceChanger, ItemPriceChanger>()
            .AddTransient<IItemAvailabilityChanger, ItemAvailabilityChanger>()
            .AddTransient<IItemRemovalService, ItemRemovalService>()
            .AddTransient<IItemDeletionKeyboardBuilder, ItemDeletionKeyboardBuilder>();
    }
}