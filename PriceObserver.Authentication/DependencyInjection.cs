using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PriceObserver.Authentication.Abstract;
using PriceObserver.Authentication.Concrete;
using PriceObserver.Authentication.Options;

namespace PriceObserver.Authentication;

public static class DependencyInjection
{
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

    public static void AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthenticationService, AuthenticationService>();
    }
}