using System;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Concrete;

public class ParserService : IParserService
{
    private readonly IShopRepository _shopRepository;
    private readonly IHtmlLoader _htmlLoader;
    private readonly IDocumentParser _documentParser;

    public ParserService(
        IShopRepository shopRepository, 
        IHtmlLoader htmlLoader, 
        IDocumentParser documentParser)
    {
        _shopRepository = shopRepository;
        _htmlLoader = htmlLoader;
        _documentParser = documentParser;
    }

    public async Task<ParsedItemServiceResult> Parse(Uri url)
    {
        var shop = _shopRepository.GetByHost(url.Host);
            
        if (shop is null)
            return ParsedItemServiceResult.Fail(ResourceKey.Parser_ShopIsNotAvailable);

        var htmlLoadResult = await _htmlLoader.Load(url);

        if (!htmlLoadResult.IsSuccess)
            return ParsedItemServiceResult.Fail(htmlLoadResult.Error);

        var htmlDocument = htmlLoadResult.Result;

        return _documentParser.Parse(shop.Key, htmlDocument);
    }
}