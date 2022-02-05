using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PriceObserver.Api.Extensions;
using PriceObserver.Api.Services.Abstract;
using PriceObserver.Api.Services.Concrete;

namespace PriceObserver.Api;

public static class DependencyInjection
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddTransient<IApiItemService, ApiItemService>();
        services.AddTransient<IShopVmBuilder, ShopVmBuilder>();
        services.AddScoped<IPriceChangesStringBuilder, PriceChangesStringBuilder>();
        services.AddTransient<IItemVmBuilder, ItemVmBuilder>();
        
        services.AddTransient<IAuthenticationService, AuthenticationService>();
    }
     
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                var privateKey = configuration.GetJwtPrivateKey();
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(privateKey));
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    IssuerSigningKey = securityKey
                };
            });
    }
}