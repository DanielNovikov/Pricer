using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Citrus;

public class CitrusContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Citrus;
    
    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "span.fz14.aic";

        var availabilityElement = document.QuerySelector<IHtmlSpanElement>(selector);
        if (availabilityElement is null)
            return true;

        return !availabilityElement.TextContent.Contains("Нет в наличии");
    }

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "div.price";

        return document.QuerySelector<IHtmlDivElement>(selector) is not null;
    }
}