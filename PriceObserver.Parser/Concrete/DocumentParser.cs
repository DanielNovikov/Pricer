using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Concrete;

public class DocumentParser : IDocumentParser
{
    private readonly IEnumerable<IContentValidator> _contentValidators;
    private readonly IEnumerable<IParser> _parsers;

    public DocumentParser(
        IEnumerable<IContentValidator> contentValidators, 
        IEnumerable<IParser> parsers)
    {
        _contentValidators = contentValidators;
        _parsers = parsers;
    }

    public ParsedItemServiceResult Parse(ShopKey providerType, IHtmlDocument html)
    {
        var contentValidator = GetContentValidator(providerType);
        var contentValidationResult = contentValidator.Validate(html);

        if (!contentValidationResult.IsSuccess)
            return ParsedItemServiceResult.Fail(contentValidationResult.Error);

        var parser = GetParser(providerType);
        var parsedItem = parser.Parse(html);
        return ParsedItemServiceResult.Success(parsedItem);
    }

    private IParser GetParser(ShopKey providerType)
    {
        return _parsers.Single(x => x.ProviderType == providerType);
    }

    private IContentValidator GetContentValidator(ShopKey providerType)
    {
        return _contentValidators.Single(p => p.ProviderType == providerType);
    }
}