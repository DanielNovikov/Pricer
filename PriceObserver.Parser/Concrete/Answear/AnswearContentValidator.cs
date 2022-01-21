using System.Linq;
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
        return document.All
            .Any(e => 
                e.TagName.ToLower() == "p" && 
                !string.IsNullOrEmpty(e.ClassName) &&
                e.ClassName.Contains("Price__currentPrice"));
    }
}