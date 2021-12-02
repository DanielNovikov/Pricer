using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.MdFashion
{
    public class MdFashionParserContentValidator : ParserProviderContentValidatorBase
    {
        public override ShopType ProviderType => ShopType.MdFashion;

        protected override bool IsPriceExists(IHtmlDocument document)
        {
            return document.All
                .Any(e => 
                    e.TagName.ToLower() == "span" &&
                    !string.IsNullOrEmpty(e.ClassName) &&
                    e.ClassName.Contains("price_current"));
        }
    }
}