using Microsoft.Extensions.DependencyInjection;
using Pricer.Common.Extensions;
using Pricer.Dialog.Callback.Handlers.Abstract;
using Pricer.Dialog.Callback.Services.Abstract;
using Pricer.Dialog.Callback.Services.Concrete;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Common.Services.Concrete;

namespace Pricer.Dialog.Callback;

public static class DependencyInjection
{
    public static IServiceCollection AddDialogCallbackHandler(this IServiceCollection services)
    {
        return services
            .AddTransient<ICallbackHandlerService, CallbackHandlerService>()
            .AddTransient<ICallbackDataBuilder, CallbackDataBuilder>()
            .AddImplementations<ICallbackHandler>();
    }
}