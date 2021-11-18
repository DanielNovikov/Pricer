using System;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete
{
    public class ParserService : IParserService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IHtmlLoader _htmlLoader;
        private readonly IParserProviderService _parserProviderService;

        public ParserService(
            IShopRepository shopRepository, 
            IHtmlLoader htmlLoader, 
            IParserProviderService parserProviderService)
        {
            _shopRepository = shopRepository;
            _htmlLoader = htmlLoader;
            _parserProviderService = parserProviderService;
        }

        public async Task<ParsedItemResult> Parse(Uri url)
        {
            var shop = await _shopRepository.GetByHost(url.Host);
            
            if (shop is null)
                return ParsedItemResult.Fail("Магазин недоступен ❌");

            var htmlLoadResult = await _htmlLoader.Load(url);

            if (!htmlLoadResult.IsSuccess)
                return ParsedItemResult.Fail(htmlLoadResult.Error);

            var htmlDocument = htmlLoadResult.Result;

            return _parserProviderService.Parse(shop.Type, htmlDocument);
        }
    }
}