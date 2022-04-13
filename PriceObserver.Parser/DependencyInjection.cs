using AngleSharp.Html.Parser;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Common.Extensions;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Concrete;

namespace PriceObserver.Parser;

public static class DependencyInjection
{
    public static IServiceCollection AddParserServices(this IServiceCollection services)
    {
        services.AddHttpClient<IHtmlLoader, HtmlLoader>();
        
        return services
            .AddScoped<IHtmlParser, HtmlParser>()
            .AddScoped<IRequestHeadersBuilder, RequestHeadersBuilder>()
            
            .AddScoped<IParser, Concrete.Parser>()
            .AddScoped<IContentValidatorService, ContentValidatorService>()
            .AddScoped<IParserProviderService, ParserProviderService>()
            
            .AddImplementations<IParserProvider>()
            .AddImplementations<IContentValidator>();
    }
}