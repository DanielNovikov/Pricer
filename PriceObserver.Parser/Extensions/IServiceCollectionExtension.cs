using AngleSharp.Html.Parser;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Abstract.Intertop;
using PriceObserver.Parser.Concrete;
using PriceObserver.Parser.Concrete.Intertop;

namespace PriceObserver.Parser.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void ConfigureParser(this IServiceCollection services)
        {
            services.AddTransient<IHtmlParser, HtmlParser>();
            services.AddHttpClient<IHtmlLoader, HtmlLoader>();

            services.AddTransient<IParserService, ParserService>();

            services.AddTransient<IParserProviderService, IntertopParserService>();
            services.AddTransient<IIntertopParserContentValidator, IntertopParserContentValidator>();
            services.AddTransient<IIntertopParser, IntertopParser>();
        }
    }
}