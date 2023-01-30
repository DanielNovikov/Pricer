using Microsoft.Extensions.DependencyInjection;
using Pricer.Bot.Abstract;
using Pricer.Bot.Concrete;

namespace Pricer.Bot;

public static class DependencyInjection
{
    public static IServiceCollection AddBotServices(this IServiceCollection services)
    {
        return services.AddScoped<IBotService, BotService>();
    }
}