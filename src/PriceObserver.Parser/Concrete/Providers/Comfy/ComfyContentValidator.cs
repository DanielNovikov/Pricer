using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Comfy;

public class ComfyContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Comfy;
    
    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "div.price__out-of-stock";

        return document.QuerySelector<IHtmlDivElement>(selector) is null;
    }

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "meta[name='product:sale_price:amount']";

        return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
    }
}