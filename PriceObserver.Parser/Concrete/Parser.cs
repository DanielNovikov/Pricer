using System;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Concrete;

public class Parser : IParser
{
    private readonly IShopRepository _shopRepository;
    private readonly IHtmlLoader _htmlLoader;
    private readonly IContentValidatorService _contentValidatorService;
    private readonly IParserProviderService _parserProviderService;

    public Parser(
        IShopRepository shopRepository, 
        IHtmlLoader htmlLoader, 
        IContentValidatorService contentValidatorService,
        IParserProviderService parserProviderService)
    {
        _shopRepository = shopRepository;
        _htmlLoader = htmlLoader;
        _contentValidatorService = contentValidatorService;
        _parserProviderService = parserProviderService;
    }

    public async Task<ParsedItemServiceResult> Parse(Uri url, ShopKey key)
    {
        var htmlLoadResult = await _htmlLoader.Load(url);

        if (!htmlLoadResult.IsSuccess)
            return ParsedItemServiceResult.Fail(htmlLoadResult.Error);

        var htmlDocument = htmlLoadResult.Result;
        
        var contentValidationResult = _contentValidatorService.Validate(key, htmlDocument);

        if (!contentValidationResult.IsSuccess)
            return ParsedItemServiceResult.Fail(contentValidationResult.Error);

        var parsedItem = _parserProviderService.Parse(key, htmlDocument);
        return ParsedItemServiceResult.Success(parsedItem);
    }
}