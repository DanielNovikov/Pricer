using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Makeup;

public class MakeupContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Makeup;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "span[itemprop=price]";
        
        return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
    }
}