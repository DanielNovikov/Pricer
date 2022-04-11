using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using PriceObserver.Web.App.Extensions;
using PriceObserver.Web.App.Options;
using PriceObserver.Web.App.Services.Abstract;
using PriceObserver.Web.App.Services.Concrete;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;
using PriceObserver.Web.Shared.Services.Abstract;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .AddOptions<ApiSettings>()
    .Bind(builder.Configuration.GetSection(nameof(ApiSettings)));

builder.Services
    .AddHttpClient<IAuthenticationService, AuthenticationHttpService>((serviceProvider, httpClient) =>
    {
        var optionsSnapshot = serviceProvider.GetService<IOptions<ApiSettings>>() ??
            throw new ArgumentNullException(nameof(ApiSettings));

        httpClient.BaseAddress = new Uri(optionsSnapshot.Value.BaseAddress);
    });

builder.Services
    .AddGrpcWebClient<Authentication.AuthenticationClient>()
    .AddScoped<IAuthenticationHandlerService, AuthenticationHandlerService>()
    .AddGrpcWebClient<DeleteItem.DeleteItemClient>()
    .AddScoped<IDeleteItemHandlerService, DeleteItemHandlerService>()
    .AddGrpcWebClient<GetItems.GetItemsClient>()
    .AddScoped<IGetItemsHandlerService, GetItemsHandlerService>();

builder.Services
    .AddScoped<ICookieManager, CookieManager>()
    .AddScoped<IMetadataBuilder, MetadataBuilder>();

await builder.Build().RunAsync();