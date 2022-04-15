using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Data.Service.Concrete;

namespace PriceObserver.Data.Service;

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
            .AddScoped<IItemParseResultService, ItemParseResultService>();
    }
}