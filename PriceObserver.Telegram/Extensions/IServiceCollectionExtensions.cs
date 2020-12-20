using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Model.Telegram.Options;
using PriceObserver.Telegram.Abstract.Client;
using PriceObserver.Telegram.Abstract.Commands;
using PriceObserver.Telegram.Abstract.Commands.Add;
using PriceObserver.Telegram.Abstract.Commands.All;
using PriceObserver.Telegram.Abstract.Commands.Remove;
using PriceObserver.Telegram.Concrete.Client;
using PriceObserver.Telegram.Concrete.Commands;
using PriceObserver.Telegram.Concrete.Commands.Add;
using PriceObserver.Telegram.Concrete.Commands.All;
using PriceObserver.Telegram.Concrete.Commands.Remove;

namespace PriceObserver.Telegram.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureTelegram(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddOptions<TelegramClientOptions>()
                .Bind(configuration.GetSection("TelegramClient"));

            services.AddSingleton<ITelegramBot, TelegramBot>();
            services.AddTransient<ITelegramBotService, TelegramBotService>();
            services.AddSingleton<ITelegramBotProcessor, TelegramBotProcessor>();

            services.AddTransient<ICommandProcessor, CommandProcessor>();
            
            services.AddTransient<IAddCommandParametersBuilder, AddCommandParametersBuilder>();
            services.AddTransient<IAddCommandService, AddCommandService>();
            services.AddTransient<IItemBuilder, ItemBuilder>();
            
            services.AddTransient<IAllCommandService, AllCommandService>();
            
            services.AddTransient<IRemoveCommandParametersBuilder, RemoveCommandParametersBuilder>();
            services.AddTransient<IRemoveCommandService, RemoveCommandService>();
            
            services.AddTransient<ICommandParameterParser, CommandParameterParser>();
            services.AddTransient<ICommandParametersParser, CommandParametersParser>();
            
            services.ConfigureCommands();
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static void ConfigureCommands(this IServiceCollection services)
        {
            var commandType = typeof(ICommand);

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