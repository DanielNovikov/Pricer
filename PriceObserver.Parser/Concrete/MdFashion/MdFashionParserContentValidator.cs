using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.MdFashion
{
    public class MdFashionParserContentValidator : ParserProviderContentValidatorBase
    {
        public override ShopType ProviderType => ShopType.MdFashion;

        protected override bool IsPriceExists(IList<IElement> elements)
        {
            return elements
                .Any(e => 
                    e.TagName.ToLower() == "span" &&
                    !string.IsNullOrEmpty(e.ClassName) &&
                    e.ClassName.Contains("price_current"));
        }
    }
}