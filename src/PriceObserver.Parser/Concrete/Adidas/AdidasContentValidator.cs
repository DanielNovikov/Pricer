using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Adidas;

public class AdidasContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Adidas;

    public bool IsAvailable(IHtmlDocument document)
    {
        return true;
    }

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string discountPriceSelector = "div.product__sidebar__inner > div > span.product__price--sale";
        const string fullPriceSelector = "div.product__sidebar__inner > div > span.product__price--first";

        return 
            document.QuerySelector<IHtmlSpanElement>(discountPriceSelector) is not null ||
            document.QuerySelector<IHtmlSpanElement>(fullPriceSelector) is not null;
    }
}