using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Telegram.Abstract;
using PriceObserver.Telegram.Concrete;
using PriceObserver.Telegram.Options;

namespace PriceObserver.Telegram;

public static class DependencyInjection
{
    public static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<TelegramClientOptions>()
            .Bind(configuration.GetSection("TelegramClient"));

        return services
            .AddSingleton<ITelegramBot, TelegramBot>()
            .AddTransient<ITelegramBotService, TelegramBotService>()
            .AddTransient<IUpdateHandler, UpdateHandler>()
            .Decorate<IUpdateHandler, UpdateExceptionHandler>()
            .AddTransient<IReplyKeyboardMarkupBuilder, ReplyKeyboardMarkupBuilder>();
    }
}