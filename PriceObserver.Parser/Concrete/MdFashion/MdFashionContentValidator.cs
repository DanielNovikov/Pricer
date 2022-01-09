using System.Linq;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.MdFashion;

public class MdFashionContentValidator : ContentValidatorBase
{
    public override ShopKey ProviderType => ShopKey.MdFashion;

    protected override bool IsPriceExists(IHtmlDocument document)
    {
        return document.All
            .Any(e => 
                e.TagName.ToLower() == "span" &&
                !string.IsNullOrEmpty(e.ClassName) &&
                e.ClassName.Contains("price_current"));
    }
}