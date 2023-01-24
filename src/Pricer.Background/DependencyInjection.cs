using Microsoft.Extensions.DependencyInjection;
using Pricer.Background.Jobs;
using Pricer.Background.Services.Abstract;
using Pricer.Background.Services.Concrete;

namespace Pricer.Background;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        return services
            .AddHostedService<ItemsObserver>()
            .AddHostedService<TelegramMessageReceiver>()
            
            .AddTransient<IItemsObserverService, ItemsObserverService>()
            .AddTransient<IItemPriceService, ItemPriceService>()
            .AddTransient<IItemAvailabilityService, ItemAvailabilityService>()
            .AddTransient<IItemRemovalService, ItemRemovalService>()
            .AddTransient<IItemModificationService, ItemModificationService>()
            .AddTransient<IItemDeletionKeyboardBuilder, ItemDeletionKeyboardBuilder>();
    }
}