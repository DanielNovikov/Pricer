using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Answear;

public class AnswearContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Answear;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string discountPriceSelector = "div[class^=ProductCard] div[class^=Price__salePrice]";
        const string fullPriceSelector = "div[class^=ProductCard] div[class^=Price__price]";

        return 
            document.QuerySelector<IHtmlDivElement>(discountPriceSelector) is not null ||
            document.QuerySelector<IHtmlDivElement>(fullPriceSelector) is not null;
    }
}