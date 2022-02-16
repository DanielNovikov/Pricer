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
        services.AddScoped<IHtmlParser, HtmlParser>();
        services.AddHttpClient<IHtmlLoader, HtmlLoader>();
        services.AddScoped<IRequestHeadersBuilder, RequestHeadersBuilder>();

        services.AddScoped<IParser, Concrete.Parser>();
        services.AddScoped<IContentValidatorService, ContentValidatorService>();
        services.AddScoped<IParserProviderService, ParserProviderService>();
        
        services.AddImplementations<IParserProvider>();
        services.AddImplementations<IContentValidator>();

        return services;
    }
}