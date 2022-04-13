using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PriceObserver.Web.Api.Extensions;
using PriceObserver.Web.Api.Handlers;
using PriceObserver.Web.Api.Interceptors;
using PriceObserver.Web.Api.Services.Abstract;
using PriceObserver.Web.Api.Services.Concrete;
using PriceObserver.Web.Shared.Grpc.HandlerServices;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddConfiguredGrpc(this IServiceCollection services)
    {
        services.AddGrpc(options => 
            options.Interceptors.Add<ErrorHandlingInterceptor>());
        
        return services;
    }
    
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IShopResponseModelBuilder, ShopResponseModelBuilder>()
            .AddScoped<IPriceChangesStringBuilder, PriceChangesStringBuilder>()
            .AddTransient<IItemResponseModelBuilder, ItemResponseModelBuilder>()
            
            .AddScoped<IAuthenticationHandlerService, AuthenticationHandlerService>()
            .AddScoped<IItemDeletionHandlerService, ItemDeletionHandlerService>()
            .AddScoped<IItemsReceptionHandlerService, ItemsReceptionHandlerService>()
            
            .AddTransient<IJwtService, JwtService>()
            .AddTransient<IUserAuthenticationService, UserAuthenticationService>()
            .AddTransient<ICookieManager, CookieManager>()
            .AddHttpContextAccessor();
    }
     
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
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

        return services;
    }

    public static IEndpointRouteBuilder MapGrpcEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGrpcService<AuthenticationHandler>().EnableGrpcWeb();
        endpoints.MapGrpcService<ItemDeletionHandler>().EnableGrpcWeb();
        endpoints.MapGrpcService<ItemsReceptionHandler>().EnableGrpcWeb();

        return endpoints;
    }
}