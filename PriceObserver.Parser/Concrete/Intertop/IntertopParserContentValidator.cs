using System.Linq;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Intertop
{
    public class IntertopParserContentValidator : ParserProviderContentValidatorBase
    {
        public override ShopKey ProviderType => ShopKey.Intertop;

        protected override bool IsPriceExists(IHtmlDocument document)
        {
            return document
                .All
                .Any(e => 
                    e.TagName.ToLower() == "span" && 
                    !string.IsNullOrEmpty(e.ClassName) &&
                    e.ClassName == "price-contain");
        }
    }
}