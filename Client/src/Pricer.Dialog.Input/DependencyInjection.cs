using Microsoft.Extensions.DependencyInjection;
using Pricer.Common.Extensions;
using Pricer.Dialog.Input.Commands.Abstract;
using Pricer.Dialog.Input.Commands.Concrete;
using Pricer.Dialog.Input.Menus.Abstract;
using Pricer.Dialog.Input.Menus.Concrete;
using Pricer.Dialog.Input.Services.Abstract;
using Pricer.Dialog.Input.Services.Concrete;

namespace Pricer.Dialog.Input;

public static class DependencyInjection
{
    public static IServiceCollection AddDialogInputHandler(this IServiceCollection services)
    {
        return services
            .AddTransient<ICommandHandlerService, CommandHandlerService>()

            .AddTransient<IShopsMessageBuilder, ShopsMessageBuilder>()
            .AddTransient<IShopCategoriesMessageBuilder, ShopCategoriesMessageBuilder>()

            .AddTransient<IMessageHandler, MessageHandler>()
            .AddTransient<IUserRegistrationHandler, UserRegistrationHandler>()
            .AddTransient<IWebsiteLoginUrlBuilder, WebsiteLoginUrlBuilder>()
            .AddTransient<IWrongCommandHandler, WrongCommandHandler>()

            .AddTransient<IMenuInputHandlerService, MenuInputHandlerService>()
            .AddTransient<IUrlExtractor, UrlExtractor>()
            .AddTransient<IUserItemParser, UserItemParser>()
            .AddTransient<IUserRedirectionService, UserRedirectionService>()
            .AddTransient<IUserLanguageChanger, UserLanguageChanger>()

            .AddImplementations<ICommandHandler>()
            .AddImplementations<IMenuInputHandler>();
    }
}