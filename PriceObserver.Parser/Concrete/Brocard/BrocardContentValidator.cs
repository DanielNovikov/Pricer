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

    public bool IsPriceExists(IHtmlDocument document)
    {
        const string selector = ".price-format > .price";
        
        var span = document.QuerySelector<IHtmlSpanElement>(selector);
        if (span == null)
            return false;
        
        var spanText = span.Text();

        var price = spanText.Replace(" ", string.Empty);
        price = price[..price.IndexOf(',')];
        
        var parsedPrice = int.Parse(price);
        return parsedPrice > 0;
    }
}