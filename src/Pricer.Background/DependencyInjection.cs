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
            .AddHostedService<SeedJob>()
            .AddHostedService<ItemsObserver>()
            .AddHostedService<TelegramMessageReceiver>()
            .AddHostedService<ViberWebhookJob>()
            
            .AddTransient<IBotService, BotService>()
            .AddTransient<IItemsObserverService, ItemsObserverService>()
            .AddTransient<IItemJobService, ItemJobService>()
            .AddTransient<IItemPriceChangedKeyboardBuilder, ItemPriceChangedKeyboardBuilder>();
    }
}