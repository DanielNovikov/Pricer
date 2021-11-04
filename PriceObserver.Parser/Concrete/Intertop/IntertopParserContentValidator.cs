using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Intertop
{
    public class IntertopParserContentValidator : ParserProviderContentValidatorBase
    {
        public override ShopType ProviderType => ShopType.Intertop;

        protected override bool IsPriceExists(IList<IElement> elements)
        {
            return elements
                .Any(e => 
                    e.TagName.ToLower() == "span" && 
                    !string.IsNullOrEmpty(e.ClassName) &&
                    e.ClassName == "price-contain");
        }
    }
}