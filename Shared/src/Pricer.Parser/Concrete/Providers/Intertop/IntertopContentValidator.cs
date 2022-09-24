using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Intertop;

public class IntertopContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Intertop;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string priceElementSelector = "span[class=price-contain]";
        const string reportAvailabilitySelector = "a[id='report-availability']";

        return 
	        document.QuerySelector<IHtmlSpanElement>(priceElementSelector) is not null ||
            document.QuerySelector<IHtmlElement>(reportAvailabilitySelector) is not null;
    }
}