using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceObserver.Background;
using PriceObserver.Data;
using PriceObserver.Data.InMemory;
using PriceObserver.Data.Service;
using PriceObserver.Dialog;
using PriceObserver.Parser;
using PriceObserver.Telegram;
using PriceObserver.Web.Api;

namespace PriceObserver;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {       
        services.AddControllersWithViews();
        services.AddRazorPages();
        
        services.AddApiServices();
        services.AddJwtAuthentication(_configuration);
        
        services.AddTelegramBot(_configuration);
        services.AddDialog(_configuration);
        services.AddParserServices();
        services.AddBackgroundJobs();
            
        services.AddData();
        services.AddDataContext(_configuration);
        services.AddMemoryCache();
        services.AddInMemoryData();
        services.AddDataServices();
            
        services.AddCors();
    }

    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseWebAssemblyDebugging();
        
        app.UseExceptionHandling();
        
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();
            
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
            
        app.UseCors(builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
            
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            
            endpoints.MapControllerRoute(
                "default",
                "{controller}/{action}/{id?}");
            
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}