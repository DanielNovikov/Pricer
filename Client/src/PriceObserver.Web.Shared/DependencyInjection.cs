using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Web.Shared.Services.Abstract;
using PriceObserver.Web.Shared.Services.Concrete;

namespace PriceObserver.Web.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddPrerenderCache(this IServiceCollection services)
    {
        return services.AddScoped<IPrerenderCache, PrerenderCache>();
    }
}