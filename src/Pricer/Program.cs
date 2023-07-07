using Microsoft.AspNetCore;
using Serilog;
using TelegramSink;

namespace Pricer;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var host = CreateHostBuilder(args).Build();

            InitializeLogger();

            host.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to start application");
            
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.InnerException);
        }
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