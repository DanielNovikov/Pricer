using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PriceObserver.Web.Shared.Grpc;
using Pricer.Web.App.Extensions;
using Pricer.Web.App.Services.Abstract;
using Pricer.Web.App.Services.Concrete;
using Pricer.Web.Shared;
using Pricer.Web.Shared.Grpc.HandlerServices;
using Pricer.Web.Shared.Services.Abstract;

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
    .AddScoped<IUserAuthenticationService, UserAuthenticationService>()
    .AddPrerenderCache();

await builder.Build().RunAsync();