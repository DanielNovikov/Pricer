using Microsoft.Extensions.DependencyInjection;
using Pricer.Common.Services.Abstract;
using Pricer.Common.Services.Concrete;

namespace Pricer.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IPartnerUrlBuilder, PartnerUrlBuilder>()
            .AddScoped<IUserLanguage, UserLanguage>();
    }
}