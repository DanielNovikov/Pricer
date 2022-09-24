using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Stylus;

public class StylusContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Stylus;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "div.product-info-block div.regular-price";

        return document.QuerySelector<IHtmlDivElement>(selector) is not null;
    }
}