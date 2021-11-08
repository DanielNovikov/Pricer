using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Telegram.Dialog.Commands.Abstract;
using PriceObserver.Telegram.Dialog.Commands.Concrete;
using PriceObserver.Telegram.Dialog.Common.Abstract;
using PriceObserver.Telegram.Dialog.Common.Concrete;
using PriceObserver.Telegram.Dialog.Input.Abstract;
using PriceObserver.Telegram.Dialog.Input.Concrete;
using PriceObserver.Telegram.Dialog.Menus.Abstract;
using PriceObserver.Telegram.Dialog.Menus.Concrete;

namespace PriceObserver.Telegram.Dialog
{
    public static class DependencyInjection
    {
        public static void AddTelegramDialogServices(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandlerService, CommandHandlerService>();
            
            services.AddTransient<IChatAuthorizationService, ChatAuthorizationService>();
            services.AddTransient<INewUserHandler, NewUserHandler>();
            services.AddTransient<IShopsInfoMessageBuilder, ShopsInfoMessageBuilder>();

            services.AddTransient<IInputHandler, InputHandler>();

            services.AddTransient<IMenuInputHandlerService, MenuInputHandlerService>();
            services.AddTransient<IMenuKeyboardBuilder, MenuKeyboardBuilder>();
            services.AddTransient<IReplyWithKeyboardBuilder, ReplyWithKeyboardBuilder>();
            services.AddTransient<IUrlExtractor, UrlExtractor>();
            
            services.AddCommandHandlers();
            services.AddMenuHandlers();
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static void AddCommandHandlers(this IServiceCollection services)
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
        
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static void AddMenuHandlers(this IServiceCollection services)
        {
            var commandType = typeof(IMenuInputHandler);

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