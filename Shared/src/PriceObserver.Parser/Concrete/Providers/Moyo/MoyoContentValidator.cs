using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Moyo;

public class MoyoContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Moyo;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "meta[itemprop=price]";

        return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
    }
}