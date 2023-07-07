using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
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
using Pricer.Services.Abstract;
using Pricer.Services.Concrete;

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
            .AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo("/app/keys"));
        
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

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserItemsService, UserItemsService>();
        
        services.AddControllers().AddNewtonsoftJson();
        services.AddCors();
    }

    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseStaticFiles();
        
        app.UseRouting();
            
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