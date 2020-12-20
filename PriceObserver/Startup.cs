using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceObserver.Data.Extensions;
using PriceObserver.Jobs.Extensions;
using PriceObserver.Model.Extensions;
using PriceObserver.Parser.Extensions;
using PriceObserver.Telegram.Abstract.Client;
using PriceObserver.Telegram.Extensions;

namespace PriceObserver
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {   
            services.ConfigureTelegram(_configuration);
            services.ConfigureParser();
            services.ConfigureData(_configuration);
            services.ConfigureConverters();
            services.ConfigureBackgroundServices();
            
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ITelegramBotProcessor telegramBotProcessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            telegramBotProcessor.StartProcessing();
            
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
 
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
 
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}