using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Data.Service.Concrete;

namespace PriceObserver.Data.Service
{
    public static class DependencyInjection
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }
    }
}