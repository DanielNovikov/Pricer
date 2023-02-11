using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pricer.Background;
using Pricer.Bot;
using Pricer.Common;
using Pricer.Data.InMemory;
using Pricer.Data.Persistent;
using Pricer.Data.Service;
using Pricer.Dialog;
using Pricer.Parser;

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
        services
            .AddRazorPages().Services
            .AddServerSideBlazor().AddCircuitOptions(options => options.DetailedErrors = true);
        
        services
            .AddCommonServices()
            .AddBotServices(_configuration)
            .AddDialogServices(_configuration)
            .AddParserServices(_configuration)
            .AddBackgroundJobs();
            
        services
            .AddPersistentDataRepositories()
            .AddInMemoryDataRepositories()
            .AddDbContext(_configuration)
            .AddMemoryCache()
            .AddDataServices();

        services.AddControllers().AddNewtonsoftJson();
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
        app.UseStaticFiles(new StaticFileOptions
        {
            ServeUnknownFileTypes = true
        });
        
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
            
        app.UseCors(builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
            
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller}/{action}/{id?}");

            endpoints.MapBlazorHub();

            endpoints.MapRazorPages();

            endpoints.MapFallbackToPage("/_Host");
        });
    }
}