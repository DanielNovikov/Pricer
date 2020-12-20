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

        public async Task<ParsedItem> Parse(Uri url)
        {
            var parserProxy = _parserProxies.FirstOrDefault(p => p.Host == url.Host);
            
            if (parserProxy == null)
                throw new Exception("Shop is not available");

            var htmlDocument = await _htmlLoader.Load(url);
            
            return parserProxy.Parse(htmlDocument);
        }
    }
}