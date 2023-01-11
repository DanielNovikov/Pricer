using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Telemart;

public class TelemartContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Telemart;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string priceElementSelector = "meta[itemprop=price]";
        const string imageElementSelector = "meta[property='og:image']";

        return
            document.QuerySelector<IHtmlMetaElement>(priceElementSelector) is not null ||
            document.QuerySelector<IHtmlMetaElement>(imageElementSelector) is not null;
    }
}