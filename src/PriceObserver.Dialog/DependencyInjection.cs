using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Common.Extensions;
using PriceObserver.Common.Models.Options;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Concrete;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Menus.Concrete;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Concrete;

namespace PriceObserver.Dialog;

public static class DependencyInjection
{
    public static IServiceCollection AddDialogServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<WebsiteOptions>()
            .Bind(configuration.GetSection(nameof(WebsiteOptions)));
        
        return services
            .AddTransient<ICommandHandlerService, CommandHandlerService>()
            
            .AddTransient<IShopsInfoMessageBuilder, ShopsInfoMessageBuilder>()
            .AddTransient<IUserActionLogger, UserActionLogger>()
            
            .AddTransient<IInputHandler, InputHandler>()
            .AddTransient<IAuthorizationService, AuthorizationService>()
            .AddTransient<IUserRegistrationHandler, UserRegistrationHandler>()
            .AddTransient<IWebsiteLoginUrlBuilder, WebsiteLoginUrlBuilder>()
            .AddTransient<IWrongCommandHandler, WrongCommandHandler>()
            
            .AddTransient<IMenuInputHandlerService, MenuInputHandlerService>()
            .AddTransient<IMenuKeyboardBuilder, MenuKeyboardBuilder>()
            .AddTransient<IReplyWithKeyboardBuilder, ReplyWithKeyboardBuilder>()
            .AddTransient<IUrlExtractor, UrlExtractor>()
            .AddTransient<IUserItemParser, UserItemParser>()
            .AddTransient<IUserRedirectionService, UserRedirectionService>()
            .AddTransient<IUserLanguageChanger, UserLanguageChanger>()
            
            .AddImplementations<ICommandHandler>()
            .AddImplementations<IMenuInputHandler>();
    }
}