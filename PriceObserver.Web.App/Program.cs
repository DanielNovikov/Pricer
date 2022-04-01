using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Web.App.Services.Concrete;
using PriceObserver.Web.Shared.Services.Abstract;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTransient<IAuthenticationService, AuthenticationHttpService>();
builder.Services.AddTransient<ICookieManager, CookieManager>();
builder.Services.AddHttpContextAccessor();

await builder.Build().RunAsync();