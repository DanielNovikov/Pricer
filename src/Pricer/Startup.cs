using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceObserver.Background;
using PriceObserver.Data.InMemory;
using PriceObserver.Data.Persistent;
using PriceObserver.Data.Service;
using PriceObserver.Web.Shared;
using Pricer.Common;
using Pricer.Dialog;
using Pricer.Parser;
using Pricer.Telegram;
using Pricer.Web.Api;

namespace Pricer;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        
        services
            .AddApiServices()
            .AddPrerenderCache()
            .AddJwtAuthentication(_configuration);
        
        services
            .AddCommonServices()
            .AddTelegramBot(_configuration)
            .AddDialogServices(_configuration)
            .AddParserServices()
            .AddBackgroundJobs();
            
        services
            .AddPersistentDataRepositories()
            .AddInMemoryDataRepositories()
            .AddDbContext(_configuration)
            .AddMemoryCache()
            .AddDataServices();

        services.AddConfiguredGrpc();
        services.AddCors();
    }

    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseWebAssemblyDebugging();
        
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();
            
        app.UseRouting();
        app.UseGrpcWeb();

        app.UseAuthentication();
        app.UseAuthorization();
            
        app.UseCors(builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
            
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            
            endpoints.MapGrpcEndpoints();

            endpoints.MapFallbackToPage("/_Host");
        });
    }
}