using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PriceObserver.Web.Api.Extensions;
using PriceObserver.Web.Api.Handlers;
using PriceObserver.Web.Api.Services.Abstract;
using PriceObserver.Web.Api.Services.Concrete;
using PriceObserver.Web.Shared.Grpc.HandlerServices;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.Api;

public static class DependencyInjection
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddTransient<IShopResponseModelBuilder, ShopResponseModelBuilder>();
        services.AddScoped<IPriceChangesStringBuilder, PriceChangesStringBuilder>();
        services.AddTransient<IItemResponseModelBuilder, ItemResponseModelBuilder>();
        
        services.AddScoped<IAuthenticationHandlerService, AuthenticationHandlerService>();
        services.AddScoped<IItemDeletionHandlerService, ItemDeletionHandlerService>();
        services.AddScoped<IItemsReceptionHandlerService, ItemsReceptionHandlerService>();
        
        services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();
        services.AddTransient<ICookieManager, CookieManager>();
        services.AddHttpContextAccessor();
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

    public static void UseExceptionHandling(this IApplicationBuilder app)
    {   
        app.UseExceptionHandler("/api/error/handle");
    }

    public static void MapGrpcEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGrpcService<AuthenticationHandler>().EnableGrpcWeb();
        endpoints.MapGrpcService<ItemDeletionHandler>().EnableGrpcWeb();
        endpoints.MapGrpcService<ItemsReceptionHandler>().EnableGrpcWeb();
    }
}