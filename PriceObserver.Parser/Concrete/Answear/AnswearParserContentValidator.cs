using System.Linq;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Answear;

public class AnswearParserContentValidator : ParserProviderContentValidatorBase
{
    public override ShopKey ProviderType => ShopKey.Answear;

    protected override bool IsPriceExists(IHtmlDocument document)
    {
        return document.All
            .Any(e => 
                e.TagName.ToLower() == "p" && 
                !string.IsNullOrEmpty(e.ClassName) &&
                e.ClassName.Contains("Price__currentPrice"));
    }
}