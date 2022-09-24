using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.MdFashion;

public class MdFashionContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.MdFashion;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string priceElementSelector = "span[class~=price_current]";
        const string imageElementSelector = "div[id=product0] img";
        
        return
            document.QuerySelector<IHtmlSpanElement>(priceElementSelector) is not null ||
            document.QuerySelector<IHtmlImageElement>(imageElementSelector) is not null;
    }
}