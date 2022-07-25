using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Citrus;

public class CitrusContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Citrus;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "div.price";

        return document.QuerySelector<IHtmlDivElement>(selector) is not null;
    }
}