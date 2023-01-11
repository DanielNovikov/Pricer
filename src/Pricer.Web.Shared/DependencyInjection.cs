using Microsoft.Extensions.DependencyInjection;
using Pricer.Web.Shared.Services.Abstract;
using Pricer.Web.Shared.Services.Concrete;

namespace Pricer.Web.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddPrerenderCache(this IServiceCollection services)
    {
        return services.AddScoped<IPrerenderCache, PrerenderCache>();
    }
}