using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Farfetch;

public class FarfetchContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Farfetch;

    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "div[data-tstid=outOfStock]";

        return document.QuerySelector<IHtmlDivElement>(selector) is null;
    }

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string discountPriceSelector = "span[data-tstid=priceInfo-onsale]";
        const string fullPriceSelector = "span[data-tstid=priceInfo-original]";

        return 
            document.QuerySelector<IHtmlElement>(discountPriceSelector) is not null ||
            document.QuerySelector<IHtmlElement>(fullPriceSelector) is not null;
    }
}