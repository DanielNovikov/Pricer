using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pricer.Telegram.Abstract;
using Pricer.Telegram.Concrete;
using Pricer.Telegram.Options;

namespace Pricer.Telegram;

public static class DependencyInjection
{
    public static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<TelegramClientOptions>()
            .Bind(configuration.GetSection("TelegramClient"));

        return services
            .AddSingleton<ITelegramBot, TelegramBot>()
            .AddTransient<ITelegramBotService, TelegramBotService>();
    }
}