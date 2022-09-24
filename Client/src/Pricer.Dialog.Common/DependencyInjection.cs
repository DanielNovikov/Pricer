using Microsoft.Extensions.DependencyInjection;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Common.Services.Concrete;

namespace Pricer.Dialog.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddDialogCommonServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IUserActionLogger, UserActionLogger>()
            .AddTransient<IAuthorizationService, AuthorizationService>()
            .AddTransient<IMenuKeyboardBuilder, MenuKeyboardBuilder>();
    }
}