using Microsoft.Extensions.DependencyInjection;
using Pricer.Data.Service.Abstract;
using Pricer.Data.Service.Concrete;

namespace Pricer.Data.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IItemService, ItemService>()
            .AddScoped<IUserTokenService, UserTokenService>()
            .AddScoped<IResourceService, ResourceService>()
            .AddScoped<IMenuService, MenuService>()
            .AddScoped<ICommandService, CommandService>()
            .AddScoped<IItemParseResultService, ItemParseResultService>()
            .AddScoped<ICurrencyService, CurrencyService>();
    }
}