using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceObserver.Background;
using PriceObserver.Data;
using PriceObserver.Data.Service;
using PriceObserver.Model.Converters;
using PriceObserver.Parser;
using PriceObserver.Telegram.Client;
using PriceObserver.Telegram.Client.Abstract;
using PriceObserver.Telegram.Dialog;

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
            services.AddTelegramBot(_configuration);
            services.AddTelegramDialogServices();
            services.AddParserServices();
            services.AddData(_configuration);
            services.AddDataServices();
            services.AddConverters();
            services.AddBackgroundJobs();
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
        }
    }
}