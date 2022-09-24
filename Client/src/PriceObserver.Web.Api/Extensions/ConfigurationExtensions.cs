using Microsoft.Extensions.Configuration;

namespace PriceObserver.Web.Api.Extensions;

public static class ConfigurationExtensions
{
    public static string GetJwtPrivateKey(this IConfiguration configuration)
    {
        return configuration.GetValue<string>("JwtOptions:PrivateKey");
    }
}