using System.Linq;
using System.Reflection;
using AngleSharp.Html.Parser;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Abstract.Answear;
using PriceObserver.Parser.Abstract.Intertop;
using PriceObserver.Parser.Abstract.MdFashion;
using PriceObserver.Parser.Concrete;
using PriceObserver.Parser.Concrete.Answear;
using PriceObserver.Parser.Concrete.Intertop;
using PriceObserver.Parser.Concrete.MdFashion;

namespace PriceObserver.Parser
{
    public static class DependencyInjection
    {
        public static void AddParserServices(this IServiceCollection services)
        {
            services.AddTransient<IHtmlParser, HtmlParser>();
            services.AddHttpClient<IHtmlLoader, HtmlLoader>();

            services.AddTransient<IParserService, ParserService>();

            services.AddTransient<IIntertopParserContentValidator, IntertopParserContentValidator>();
            services.AddTransient<IIntertopParser, IntertopParser>();
            
            services.AddTransient<IMdFashionParserContentValidator, MdFashionParserContentValidator>();
            services.AddTransient<IMdFashionParser, MdFashionParser>();

            services.AddTransient<IAnswearParserContentValidator, AnswearParserContentValidator>();
            services.AddTransient<IAnswearParser, AnswearParser>();
            
            services.AddParserProviderServices();
        }
        
        private static void AddParserProviderServices(this IServiceCollection services)
        {
            var parserProviderService = typeof(IParserProviderService);

            var commandImplementations = Assembly
                .GetExecutingAssembly()
                .DefinedTypes
                .Where(type => parserProviderService.IsAssignableFrom(type) && parserProviderService != type)
                .ToList();

            commandImplementations.ForEach(commandImplementation =>
            {
                services.AddTransient(parserProviderService, commandImplementation);
            });
        }
    }
}