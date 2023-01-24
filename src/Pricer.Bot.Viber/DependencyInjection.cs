using Microsoft.Extensions.DependencyInjection;
using Pricer.Viber.Services.Abstract;
using Pricer.Viber.Services.Concrete;

namespace Pricer.Viber;

public static class DependencyInjection
{
    public static IServiceCollection AddViberBot(this IServiceCollection services)
    {
        services.AddHttpClient<IViberBotService, ViberBotService>(client =>
        {
            client.BaseAddress = new Uri("https://chatapi.viber.com");
            client.DefaultRequestHeaders.Add("X-Viber-Auth-Token", "50687d871a67e550-450f5771df432363-9852ebf617b5d72");
        });

        return services
            .AddTransient<IViberWebhookHandler, ViberWebhookHandler>()
            .AddTransient<IKeyboardButtonsBuilder, KeyboardButtonsBuilder>();
    }
}