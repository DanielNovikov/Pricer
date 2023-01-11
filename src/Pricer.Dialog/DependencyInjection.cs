using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pricer.Common.Extensions;
using Pricer.Common.Models.Options;
using Pricer.Dialog.Callbacks.Abstract;
using Pricer.Dialog.Callbacks.Concrete;
using Pricer.Dialog.Commands.Abstract;
using Pricer.Dialog.Commands.Concrete;
using Pricer.Dialog.Menus.Abstract;
using Pricer.Dialog.Menus.Concrete;
using Pricer.Dialog.Services.Abstract;
using Pricer.Dialog.Services.Concrete;

namespace Pricer.Dialog;

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
            
            .AddTransient<IShopsMessageBuilder, ShopsMessageBuilder>()
            .AddTransient<IShopCategoriesMessageBuilder, ShopCategoriesMessageBuilder>()
            .AddTransient<IUserActionLogger, UserActionLogger>()
            
            .AddTransient<IMessageHandler, MessageHandler>()
            .AddTransient<IAuthorizationService, AuthorizationService>()
            .AddTransient<IUserRegistrationHandler, UserRegistrationHandler>()
            .AddTransient<IWebsiteLoginUrlBuilder, WebsiteLoginUrlBuilder>()
            .AddTransient<IWrongCommandHandler, WrongCommandHandler>()
            
            .AddTransient<IMenuInputHandlerService, MenuInputHandlerService>()
            .AddTransient<IMenuKeyboardBuilder, MenuKeyboardBuilder>()
            .AddTransient<IUrlExtractor, UrlExtractor>()
            .AddTransient<IUserItemParser, UserItemParser>()
            .AddTransient<IUserRedirectionService, UserRedirectionService>()
            .AddTransient<IUserLanguageChanger, UserLanguageChanger>()
            .AddTransient<ICallbackHandlerService, CallbackHandlerService>()
            .AddTransient<ICallbackDataBuilder, CallbackDataBuilder>()
            
            .AddImplementations<ICommandHandler>()
            .AddImplementations<IMenuInputHandler>()
            .AddImplementations<ICallbackHandler>();
    }
}