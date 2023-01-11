using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.IHerb;

public class IHerbContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.IHerb;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "div.product-summary-title";

        return document.QuerySelector<IHtmlDivElement>(selector) is not null;
    }
}