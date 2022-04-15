using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Answear;

public class AnswearContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Answear;

    public bool IsAvailable(IHtmlDocument document)
    {
        return true;
    }

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "div[class*=container-fluid] p[class^=Price__currentPrice_]";

        return document.QuerySelector<IHtmlParagraphElement>(selector) is not null;
    }
}