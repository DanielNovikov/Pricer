using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete
{
    public class ParserService : IParserService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IEnumerable<IParserProviderService> _parserServices;
        private readonly IHtmlLoader _htmlLoader;

        public ParserService(
            IShopRepository shopRepository,
            IEnumerable<IParserProviderService> parserServices, 
            IHtmlLoader htmlLoader)
        {
            _shopRepository = shopRepository;
            _parserServices = parserServices;
            _htmlLoader = htmlLoader;
        }

        public async Task<ParsedItemResult> Parse(Uri url)
        {
            var shop = await _shopRepository.GetByHost(url.Host);
            
            if (shop == null)
                return ParsedItemResult.Fail("Магазин недоступен ❌");

            var htmlLoadResult = await _htmlLoader.Load(url);

            if (!htmlLoadResult.IsSuccess)
                return ParsedItemResult.Fail(htmlLoadResult.Error);
            
            var parserService = _parserServices.First(p => p.ProviderType == shop.Type);
            return parserService.Parse(htmlLoadResult.Result);
        }
    }
}