using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Common.Extensions;
using PriceObserver.Common.Models.Options;
using PriceObserver.Dialog.Callbacks.Abstract;
using PriceObserver.Dialog.Callbacks.Concrete;
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
            .AddTransient<IReplyWithKeyboardBuilder, ReplyWithKeyboardBuilder>()
            .AddTransient<IUrlExtractor, UrlExtractor>()
            .AddTransient<IUserItemParser, UserItemParser>()
            .AddTransient<IUserRedirectionService, UserRedirectionService>()
            .AddTransient<IUserLanguageChanger, UserLanguageChanger>()
            .AddTransient<IUserBackgroundSettingsService, UserBackgroundSettingsService>()
            .AddTransient<ICallbackHandlerService, CallbackHandlerService>()
            .AddTransient<ICallbackDataBuilder, CallbackDataBuilder>()
            
            .AddImplementations<ICommandHandler>()
            .AddImplementations<IMenuInputHandler>()
            .AddImplementations<ICallbackHandler>();
    }
}