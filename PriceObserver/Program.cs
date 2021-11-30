using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Data;
using PriceObserver.Data.Seed;
using Serilog;
using TelegramSink;

namespace PriceObserver
{
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
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSerilog()
                .UseUrls("http://*:5000")
                .UseStartup<Startup>();

        private static void SeedData(IWebHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetService<ApplicationDbContext>();
            
            context!.Database.Migrate();
                
            DbSeeder.Seed(context);
        }
        
        private static void InitializeLogger()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets(Assembly.GetExecutingAssembly())
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
}