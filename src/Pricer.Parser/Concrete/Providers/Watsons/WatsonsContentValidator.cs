using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Watsons;

public class WatsonsContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Watsons;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "p.pdp-details__price";

        return document.QuerySelector<IHtmlParagraphElement>(selector) is not null;
    }
}