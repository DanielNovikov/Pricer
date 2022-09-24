using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pricer.Common.Models.Options;
using Pricer.Common.Services.Abstract;
using Pricer.Common.Services.Concrete;

namespace Pricer.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<WebsiteOptions>()
            .Bind(configuration.GetSection(nameof(WebsiteOptions)));
        
        return services
            .AddScoped<IPartnerUrlBuilder, PartnerUrlBuilder>()
            .AddScoped<IUserLanguage, UserLanguage>();
    }
}