using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceObserver.Authentication;
using PriceObserver.Background;
using PriceObserver.Data;
using PriceObserver.Data.InMemory;
using PriceObserver.Data.Service;
using PriceObserver.Dialog;
using PriceObserver.Parser;
using PriceObserver.Telegram;

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
            services.AddControllersWithViews();
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "wwwroot"; });
            
            services.AddJwtAuthentication();
            services.AddAuthenticationServices();
            
            services.AddTelegramBot(_configuration);
            services.AddTelegramDialogServices();
            services.AddParserServices();
            services.AddBackgroundJobs();
            
            services.AddData(_configuration);
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
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            
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
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action}/{id?}");
            });

            if (!env.IsDevelopment())
            {
                app.UseSpa(builder => builder.Options.SourcePath = "wwwroot");
            }
        }
    }
}