using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete
{
    public class ParserService : IParserService
    {
        private readonly IEnumerable<IParserProviderService> _parserProxies;
        private readonly IHtmlLoader _htmlLoader;
        
        public ParserService(
            IEnumerable<IParserProviderService> parserProxies, 
            IHtmlLoader htmlLoader)
        {
            _parserProxies = parserProxies;
            _htmlLoader = htmlLoader;
        }

        public async Task<ParsedItemResult> Parse(Uri url)
        {
            var parserProxy = _parserProxies.FirstOrDefault(p => p.Host == url.Host);
            
            if (parserProxy == null)
                return ParsedItemResult.Fail("Магазин недоступен");

            var htmlLoadResult = await _htmlLoader.Load(url);

            if (htmlLoadResult.IsSuccess)
                return ParsedItemResult.Fail(htmlLoadResult.Error);
            
            return parserProxy.Parse(htmlLoadResult.Result);
        }
    }
}