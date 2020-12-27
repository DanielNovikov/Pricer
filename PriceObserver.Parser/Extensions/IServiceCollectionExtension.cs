using AngleSharp.Html.Parser;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Abstract.Intertop;
using PriceObserver.Parser.Abstract.MdFashion;
using PriceObserver.Parser.Concrete;
using PriceObserver.Parser.Concrete.Intertop;
using PriceObserver.Parser.Concrete.MdFashion;

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
            
            services.AddTransient<IParserProviderService, MdFashionParserService>();
            services.AddTransient<IMdFashionParserContentValidator, MdFashionParserContentValidator>();
            services.AddTransient<IMdFashionParser, MdFashionParser>();
        }
    }
}