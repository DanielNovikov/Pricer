using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Common.Services.Abstract;
using PriceObserver.Common.Services.Concrete;

namespace PriceObserver.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        return services.AddScoped<IPartnerUrlBuilder, PartnerUrlBuilder>();
    }
}