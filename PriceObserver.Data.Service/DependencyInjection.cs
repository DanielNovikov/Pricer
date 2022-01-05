using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Data.Service.Concrete;

namespace PriceObserver.Data.Service;

public static class DependencyInjection
{
    public static void AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IUserTokenService, UserTokenService>();
        services.AddScoped<IResourceService, ResourceService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<ICommandService, CommandService>();
        services.AddScoped<IItemParseResultService, ItemParseResultService>();
    }
}