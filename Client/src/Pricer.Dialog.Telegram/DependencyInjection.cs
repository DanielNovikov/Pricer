using Microsoft.Extensions.DependencyInjection;
using Pricer.Dialog.Telegram.Services.Abstract;
using Pricer.Dialog.Telegram.Services.Concrete;

namespace Pricer.Dialog.Telegram;

public static class DependencyInjection
{
    public static IServiceCollection AddTelegramHandler(this IServiceCollection services)
    {
        return services
            .AddTransient<IUpdateHandler, UpdateHandler>()
            .Decorate<IUpdateHandler, UpdateExceptionHandler>()
            .AddTransient<IReplyKeyboardMarkupBuilder, ReplyKeyboardMarkupBuilder>()
            .AddTransient<IInlineKeyboardMarkupBuilder, InlineKeyboardMarkupBuilder>();
    }
}