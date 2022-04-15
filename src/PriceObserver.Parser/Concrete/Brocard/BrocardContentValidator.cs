using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Brocard;

public class BrocardContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Brocard;

    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "div[class='stock unavailable']";

        return document.QuerySelector<IHtmlDivElement>(selector) is null;
    }

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = ".price-format > .price";
        
        var priceElement = document.QuerySelector<IHtmlSpanElement>(selector);
        if (priceElement is null)
            return false;
        
        var price = priceElement.TextContent;

        var priceWithoutSpaces = price.Replace(" ", string.Empty);
        var formattedPrice = priceWithoutSpaces[..priceWithoutSpaces.IndexOf(',')];
        
        return int.Parse(formattedPrice) > 0;
    }
}