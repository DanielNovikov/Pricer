using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Answear
{
    public class AnswearParserContentValidator : ParserProviderContentValidatorBase
    {
        public override ShopType ProviderType => ShopType.Answear;

        protected override bool IsPriceExists(IList<IElement> elements)
        {
            return elements
                .Any(e => 
                    e.TagName.ToLower() == "p" && 
                    !string.IsNullOrEmpty(e.ClassName) &&
                    e.ClassName.Contains("Price__currentPrice"));
        }
    }
}