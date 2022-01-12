using System.Linq;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.MdFashion;

public class MdFashionContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.MdFashion;

    public bool IsPriceExists(IHtmlDocument document)
    {
        return document.All
            .Any(e => 
                e.TagName.ToLower() == "span" &&
                !string.IsNullOrEmpty(e.ClassName) &&
                e.ClassName.Contains("price_current"));
    }
}