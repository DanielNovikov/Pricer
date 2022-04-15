using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.MdFashion;

public class MdFashionContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.MdFashion;

    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "div[data-status=not-available]";

        return document.QuerySelector<IHtmlDivElement>(selector) is null;
    }

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "span[class~=price_current]";
        
        return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
    }
}