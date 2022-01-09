using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Farfetch;

public class FarfetchContentValidator : ContentValidatorBase
{
    public override ShopKey ProviderType => ShopKey.Farfetch;
        
    protected override bool IsPriceExists(IHtmlDocument document)
    {
        const string discountPriceSelector = "span[data-tstid=priceInfo-onsale]";
        const string fullPriceSelector = "span[data-tstid=priceInfo-original]";

        return 
            document.QuerySelector<IHtmlElement>(discountPriceSelector) is not null ||
            document.QuerySelector<IHtmlElement>(fullPriceSelector) is not null;
    }
}