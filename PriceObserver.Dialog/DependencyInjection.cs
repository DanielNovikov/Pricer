using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Concrete;
using PriceObserver.Dialog.Commands.Concrete.WebsiteCommand.Options;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Menus.Concrete;
using PriceObserver.Dialog.Menus.Concrete.NewItemMenu;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Concrete;

namespace PriceObserver.Dialog;

public static class DependencyInjection
{
    public static void AddTelegramDialogServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<ICommandHandlerService, CommandHandlerService>();
            
        services.AddTransient<IShopsInfoMessageBuilder, ShopsInfoMessageBuilder>();
        services.AddTransient<IUserActionLogger, UserActionLogger>();
            
        services.AddTransient<IInputHandler, InputHandler>();
        services.AddTransient<IAuthorizationService, AuthorizationService>();
        services.AddTransient<IUserRegistrationHandler, UserRegistrationHandler>();

        services.AddTransient<IMenuInputHandlerService, MenuInputHandlerService>();
        services.AddTransient<IMenuKeyboardBuilder, MenuKeyboardBuilder>();
        services.AddTransient<IReplyWithKeyboardBuilder, ReplyWithKeyboardBuilder>();
        services.AddTransient<IUrlExtractor, UrlExtractor>();
        services.AddTransient<IUserItemParser, UserItemParser>();

        services
            .AddOptions<WebsiteCommandOptions>()
            .Bind(configuration.GetSection(nameof(WebsiteCommandOptions)));

        services.AddCommandHandlers();
        services.AddMenuHandlers();
    }

    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    private static void AddCommandHandlers(this IServiceCollection services)
    {
        var commandKey = typeof(ICommandHandler);

        var commandImplementations = Assembly
            .GetExecutingAssembly()
            .DefinedTypes
            .Where(type => commandKey.IsAssignableFrom(type) && commandKey != type)
            .ToList();

        commandImplementations.ForEach(commandImplementation =>
        {
            services.AddTransient(commandKey, commandImplementation);
        });
    }
        
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    private static void AddMenuHandlers(this IServiceCollection services)
    {
        var commandKey = typeof(IMenuInputHandler);

        var commandImplementations = Assembly
            .GetExecutingAssembly()
            .DefinedTypes
            .Where(type => commandKey.IsAssignableFrom(type) && commandKey != type)
            .ToList();

        commandImplementations.ForEach(commandImplementation =>
        {
            services.AddTransient(commandKey, commandImplementation);
        });
    }
}