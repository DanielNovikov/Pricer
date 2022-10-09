using Microsoft.Extensions.DependencyInjection;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Service.Services.Abstract;
using Pricer.Service.Services.Concrete;

namespace Pricer.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddTransient<IUserService, UserService>();
    }
}