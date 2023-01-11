using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Data.InMemory.Seed;
using PriceObserver.Data.Persistent;
using PriceObserver.Data.Persistent.Seed;
using Pricer.Common.Extensions;
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