using AngleSharp.Html.Parser;
using Microsoft.Extensions.DependencyInjection;
using Pricer.Common.Extensions;
using Pricer.Parser.Abstract;
using Pricer.Parser.Concrete;

namespace Pricer.Parser;

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