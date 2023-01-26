using Microsoft.Extensions.DependencyInjection;

namespace Pricer.Bot;

public static class DependencyInjection
{
    public static IServiceCollection AddBotServices(this IServiceCollection services)
    {
        return services;
    }
}