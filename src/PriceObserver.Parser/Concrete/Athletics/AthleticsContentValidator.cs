using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Sportmaster;

public class AthleticsContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Athletics;
    
    public bool IsAvailable(IHtmlDocument document)
    {
        return true;
    }

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "meta[property='product:price:amount']";

        return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
    }
}