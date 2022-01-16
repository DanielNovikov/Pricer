using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Intertop;

public class IntertopContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Intertop;

    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "a[id=report-availability]";

        return document.QuerySelector<IHtmlElement>(selector) is null;
    }

    public bool IsPriceExists(IHtmlDocument document)
    {
        const string selector = "span[class=price-contain]";

        return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
    }
}