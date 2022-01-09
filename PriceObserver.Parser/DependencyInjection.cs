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
        services.AddTransient<IHtmlParser, HtmlParser>();
        services.AddHttpClient<IHtmlLoader, HtmlLoader>();

        services.AddTransient<IParserService, ParserService>();
        services.AddTransient<IDocumentParser, DocumentParser>();
            
        services.AddParsers();
        services.AddParserContentValidators();
    }
        
    private static void AddParsers(this IServiceCollection services)
    {
        var parserProvider = typeof(IParser);

        var parserImplementations = Assembly
            .GetExecutingAssembly()
            .DefinedTypes
            .Where(type => 
                parserProvider.IsAssignableFrom(type) && 
                parserProvider != type && 
                !type.IsAbstract)
            .ToList();

        parserImplementations.ForEach(parser =>
        {
            services.AddTransient(parserProvider, parser);
        });
    }
        
    private static void AddParserContentValidators(this IServiceCollection services)
    {
        var parserProvider = typeof(IContentValidator);

        var parserImplementations = Assembly
            .GetExecutingAssembly()
            .DefinedTypes
            .Where(type => 
                parserProvider.IsAssignableFrom(type) && 
                parserProvider != type && 
                !type.IsAbstract)
            .ToList();

        parserImplementations.ForEach(parser =>
        {
            services.AddTransient(parserProvider, parser);
        });
    }
}