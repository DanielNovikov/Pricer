using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pricer.Common.Extensions;
using Pricer.Data.InMemory.Seed;
using Pricer.Data.Persistent;
using Pricer.Data.Persistent.Seed;
using Pricer.Viber.Services.Abstract;
using Serilog;
using TelegramSink;

namespace Pricer;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
            
        SeedData(host);
        InitializeLogger();
        SetViberWebhook(host);
            
        host.Run();
    }

    private static IWebHostBuilder CreateHostBuilder(string[] args) =>
        WebHost
            .CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddEnvironmentVariables();
            })
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseSerilog()
            .UseUrls("http://*:5000")
            .UseStartup<Startup>();

    private static void SetViberWebhook(IWebHost host)
    {
        using var scope = host.Services.CreateScope();
            
        var viberBotService = scope.GetService<IViberBotService>();

        Task.Run(async () =>
        {
            await Task.Delay(2000);
            await viberBotService.SetWebhook();
        });
    }
    
    private static void SeedData(IWebHost host)
    {
        using var scope = host.Services.CreateScope();
            
        var context = scope.GetService<ApplicationDbContext>();
        context!.Database.Migrate();
        DbSeeder.Seed(context);

        var cache = scope.GetService<IMemoryCache>();
        InMemorySeeder.Seed(cache);
    }
        
    private static void InitializeLogger()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .MinimumLevel.Verbose()
            .WriteTo.TeleSink(
                configuration.GetValue<string>("TelegramLogClient:AccessToken"),
                configuration.GetValue<string>("TelegramLogClient:UserId"))
            .CreateLogger();
    }
}