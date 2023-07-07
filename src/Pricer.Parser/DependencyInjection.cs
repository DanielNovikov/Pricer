using System;
using System.Net;
using System.Net.Http;
using AngleSharp.Html.Parser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pricer.Common.Extensions;
using Pricer.Parser.Abstract;
using Pricer.Parser.Concrete;
using Pricer.Parser.Options;

namespace Pricer.Parser;

public static class DependencyInjection
{
    public static IServiceCollection AddParserServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<ProxySettings>()
            .Bind(configuration.GetSection(nameof(ProxySettings)));

        services
            .AddHttpClient("ProxyClient")
            .ConfigurePrimaryHttpMessageHandler(serviceProvider =>
            {
                var settings = serviceProvider.GetService<IOptions<ProxySettings>>()!.Value;

                var proxy = new WebProxy
                {
                    Address = new Uri($"http://{settings.Url}:{settings.Port}"),
                    Credentials = new NetworkCredential(settings.Username, settings.Password)
                };

                return new HttpClientHandler
                {
                    Proxy = proxy
                };
            });

        services.AddHttpClient<IDefaultHttpClient, DefaultHttpClient>();
        services.AddHttpClient<IProxyHttpClient, ProxyHttpClient>("ProxyClient");

        return services
            .AddTransient<IHtmlLoader, HtmlLoader>()
            .AddScoped<IHtmlParser, HtmlParser>()
            .AddScoped<IRequestHeadersBuilder, RequestHeadersBuilder>()
            .AddTransient<IExampleItemService, ExampleItemService>()
            
            .AddScoped<IParser, Concrete.Parser>()
            .AddScoped<IContentValidatorService, ContentValidatorService>()
            .AddScoped<IParserProviderService, ParserProviderService>()
            
            .AddImplementations<IParserProvider>()
            .AddImplementations<IContentValidator>();
    }
}