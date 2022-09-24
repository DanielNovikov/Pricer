using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Concrete;

public class ParserProviderService : IParserProviderService
{
    private readonly IEnumerable<IParserProvider> _parserProviders;

    public ParserProviderService(IEnumerable<IParserProvider> parserProviders)
    {
        _parserProviders = parserProviders;
    }

    public ParsedItem Parse(ShopKey providerKey, IHtmlDocument document)
    {
        var parserProvider = _parserProviders.Single(x => x.ProviderKey == providerKey);

        var isAvailable = parserProvider.IsAvailable(document);
        var price = isAvailable ? parserProvider.GetPrice(document) : default;
        var title = parserProvider.GetTitle(document);
        var imageUrl = parserProvider.GetImageUrl(document);
        var currencyKey = parserProvider.GetCurrency(document);
        
        return new ParsedItem(providerKey, price, title, imageUrl, isAvailable, currencyKey);
    }
}