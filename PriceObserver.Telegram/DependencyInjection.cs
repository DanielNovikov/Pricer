using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Telegram.Abstract;
using PriceObserver.Telegram.Concrete;
using PriceObserver.Telegram.Options;

namespace PriceObserver.Telegram
{
    public static class DependencyInjection
    {
        public static void AddTelegramBot(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddOptions<TelegramClientOptions>()
                .Bind(configuration.GetSection("TelegramClient"));

            services.AddSingleton<ITelegramBot, TelegramBot>();
            services.AddTransient<ITelegramBotService, TelegramBotService>();
            services.AddTransient<IUpdateHandler, UpdateHandler>();
            services.Decorate<IUpdateHandler, UpdateExceptionHandler>();
            services.AddTransient<IReplyKeyboardMarkupBuilder, ReplyKeyboardMarkupBuilder>();
            
            services.ConfigureCommands();
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static void ConfigureCommands(this IServiceCollection services)
        {
            var commandType = typeof(ICommandHandler);

            var commandImplementations = Assembly
                .GetExecutingAssembly()
                .DefinedTypes
                .Where(type => commandType.IsAssignableFrom(type) && commandType != type)
                .ToList();

            commandImplementations.ForEach(commandImplementation =>
            {
                services.AddTransient(commandType, commandImplementation);
            });
        }
    }
}