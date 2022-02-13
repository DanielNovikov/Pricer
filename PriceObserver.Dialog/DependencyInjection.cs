using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Common.Extensions;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Concrete;
using PriceObserver.Dialog.Commands.Concrete.WebsiteCommand.Options;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Menus.Concrete;
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
        services.AddTransient<IUserRedirectionService, UserRedirectionService>();

        services
            .AddOptions<WebsiteCommandOptions>()
            .Bind(configuration.GetSection(nameof(WebsiteCommandOptions)));

        services.AddImplementations<ICommandHandler>();
        services.AddImplementations<IMenuInputHandler>();
    }
}