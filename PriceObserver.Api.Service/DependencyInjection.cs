using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PriceObserver.Api.Services.Abstract;
using PriceObserver.Api.Services.Concrete;
using PriceObserver.Api.Services.Options;

namespace PriceObserver.Api.Services;

public static class DependencyInjection
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddTransient<IApiItemService, ApiItemService>();
        services.AddTransient<IShopVmBuilder, ShopVmBuilder>();
        services.AddScoped<IPriceChangesStringBuilder, PriceChangesStringBuilder>();
        
        services.AddTransient<IAuthenticationService, AuthenticationService>();
    }
     
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    IssuerSigningKey = PrivateKey.GetSymmetricSecurityKey()
                };
            });
    }
}