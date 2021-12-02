using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Answear
{
    public class AnswearParserContentValidator : ParserProviderContentValidatorBase
    {
        public override ShopType ProviderType => ShopType.Answear;

        protected override bool IsPriceExists(IHtmlDocument document)
        {
            return document.All
                .Any(e => 
                    e.TagName.ToLower() == "p" && 
                    !string.IsNullOrEmpty(e.ClassName) &&
                    e.ClassName.Contains("Price__currentPrice"));
        }
    }
}