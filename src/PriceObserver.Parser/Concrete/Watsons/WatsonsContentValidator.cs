using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Watsons;

public class WatsonsContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Watsons;
    
    public bool IsAvailable(IHtmlDocument document)
    {
        return true;
    }

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "p.pdp-details__price";

        return document.QuerySelector<IHtmlParagraphElement>(selector) is not null;
    }
}