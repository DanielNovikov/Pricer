using System.Linq;
using System.Reflection;
using AngleSharp.Html.Parser;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Concrete;

namespace PriceObserver.Parser;

public static class DependencyInjection
{
    public static void AddParserServices(this IServiceCollection services)
    {
        services.AddScoped<IHtmlParser, HtmlParser>();
        services.AddHttpClient<IHtmlLoader, HtmlLoader>();
        services.AddScoped<IRequestHeadersBuilder, RequestHeadersBuilder>();

        services.AddScoped<IParser, Concrete.Parser>();
        services.AddScoped<IContentValidatorService, ContentValidatorService>();
        services.AddScoped<IParserProviderService, ParserProviderService>();
        
        services.AddParsers();
        services.AddContentValidators();
    }
        
    private static void AddParsers(this IServiceCollection services)
    {
        var parserProviderType = typeof(IParserProvider);

        var parserProviderImplementations = Assembly
            .GetExecutingAssembly()
            .DefinedTypes
            .Where(type => 
                parserProviderType.IsAssignableFrom(type) && 
                parserProviderType != type && 
                !type.IsAbstract)
            .ToList();

        parserProviderImplementations.ForEach(parser =>
        {
            services.AddTransient(parserProviderType, parser);
        });
    }
        
    private static void AddContentValidators(this IServiceCollection services)
    {
        var contentValidatorType = typeof(IContentValidator);

        var contentValidatorsImplementations = Assembly
            .GetExecutingAssembly()
            .DefinedTypes
            .Where(type => 
                contentValidatorType.IsAssignableFrom(type) && 
                contentValidatorType != type && 
                !type.IsAbstract)
            .ToList();

        contentValidatorsImplementations.ForEach(parser =>
        {
            services.AddTransient(contentValidatorType, parser);
        });
    }
}