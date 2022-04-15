using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Moyo;

public class MoyoContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Moyo;
    
    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "div.noinstock-status";

        return document.QuerySelector<IHtmlDivElement>(selector) is null;
    }

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "meta[itemprop=price]";

        return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
    }
}