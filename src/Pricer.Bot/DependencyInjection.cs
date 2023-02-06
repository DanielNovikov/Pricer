using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pricer.Bot.Abstract;
using Pricer.Bot.Telegram.Abstract;
using Pricer.Bot.Telegram.Concrete;
using Pricer.Bot.Telegram.Options;
using Pricer.Bot.Viber.Models.Options;
using Pricer.Bot.Viber.Services.Abstract;
using Pricer.Bot.Viber.Services.Concrete;

namespace Pricer.Bot;

public static class DependencyInjection
{
    public static IServiceCollection AddBotServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<TelegramClientOptions>()
            .Bind(configuration.GetSection("TelegramClient"));

        services
            .AddOptions<ViberSettings>()
            .Bind(configuration.GetSection(nameof(ViberSettings)));

        services
            .AddHttpClient("ViberClient")
            .ConfigureHttpClient((serviceProvider, client) =>
            {
                var settings = serviceProvider.GetService<IOptions<ViberSettings>>()!.Value;
                
                client.BaseAddress = new Uri("https://chatapi.viber.com");
                client.DefaultRequestHeaders.Add("X-Viber-Auth-Token", settings.AuthToken);
            });
        
        services.AddHttpClient<IViberBotService, ViberBotService>("ViberClient");
        services.AddHttpClient<IBotProviderService, ViberBotService>("ViberClient");

        return services
            .AddTransient<IViberMessageHandler, ViberMessageHandler>()
            .AddTransient<IViberCallbackHandler, ViberCallbackHandler>()
            .AddTransient<IViberWebhookHandler, ViberWebhookHandler>()
            .AddTransient<IKeyboardButtonsBuilder, KeyboardButtonsBuilder>()
            .AddTransient<IRichMediaBuilder, RichMediaBuilder>()
            
            .AddTransient<ITelegramMessageHandler, TelegramMessageHandler>()
            .AddTransient<ITelegramCallbackHandler, TelegramCallbackHandler>()
            .AddSingleton<ITelegramBot, TelegramBot>()
            .AddTransient<ITelegramBotService, TelegramBotService>()
            .AddTransient<IBotProviderService, TelegramBotService>()
            .AddTransient<IUpdateHandler, UpdateHandler>()
            .Decorate<IUpdateHandler, UpdateExceptionHandler>()
            .AddTransient<IReplyMarkupBuilder, ReplyMarkupBuilder>()
            .AddTransient<IInlineKeyboardMarkupBuilder, InlineKeyboardMarkupBuilder>();
    }
}