using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Telemart;

public class TelemartContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Telemart;
    
    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = ".b-product-mpr-info .b-i-product-available-gray";

        return document.QuerySelector<IHtmlDivElement>(selector) is null;
    }

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "meta[itemprop=price]";

        return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
    }
}