using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PriceObserver.Web.App.Extensions;
using PriceObserver.Web.App.Services.Abstract;
using PriceObserver.Web.App.Services.Concrete;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;
using PriceObserver.Web.Shared.Services.Abstract;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .AddGrpcWebClient<Authentication.AuthenticationClient>()
    .AddScoped<IAuthenticationHandlerService, AuthenticationHandlerService>()
    .AddGrpcWebClient<ItemDeletion.ItemDeletionClient>()
    .AddScoped<IItemDeletionHandlerService, ItemDeletionHandlerService>()
    .AddGrpcWebClient<ItemsReception.ItemsReceptionClient>()
    .AddScoped<IItemsReceptionHandlerService, ItemsReceptionHandlerService>();

builder.Services
    .AddScoped<ICookieManager, CookieManager>()
    .AddScoped<IMetadataBuilder, MetadataBuilder>()
    .AddScoped<IUserAuthenticationService, UserAuthenticationService>();

await builder.Build().RunAsync();