using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pricer.Bot.Abstract;
using Pricer.Bot.Telegram.Abstract;
using Pricer.Bot.Telegram.Concrete;
using Pricer.Bot.Telegram.Options;
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

        services.AddHttpClient<IViberBotService, ViberBotService>(client =>
        {
            client.BaseAddress = new Uri("https://chatapi.viber.com");
            client.DefaultRequestHeaders.Add("X-Viber-Auth-Token", "50687d871a67e550-450f5771df432363-9852ebf617b5d72");
        });
        
        services.AddHttpClient<IBotProviderService, ViberBotService>(client =>
        {
            client.BaseAddress = new Uri("https://chatapi.viber.com");
            client.DefaultRequestHeaders.Add("X-Viber-Auth-Token", "50687d871a67e550-450f5771df432363-9852ebf617b5d72");
        });

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