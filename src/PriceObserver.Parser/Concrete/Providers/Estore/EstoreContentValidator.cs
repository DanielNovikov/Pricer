using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Estore;

public class EstoreContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Estore;
    
    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "p.availability.out-of-stock";
        
        return document.QuerySelector<IHtmlParagraphElement>(selector) is null;
    }

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "meta[itemprop=price]";

        return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
    }
}