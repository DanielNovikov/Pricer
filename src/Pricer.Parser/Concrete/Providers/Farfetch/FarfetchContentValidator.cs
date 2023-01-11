using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Farfetch;

public class FarfetchContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Farfetch;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string discountPriceSelector = "span[data-tstid=priceInfo-onsale]";
        const string fullPriceSelector = "span[data-tstid=priceInfo-original]";

        return 
            document.QuerySelector<IHtmlElement>(discountPriceSelector) is not null ||
            document.QuerySelector<IHtmlElement>(fullPriceSelector) is not null;
    }
}