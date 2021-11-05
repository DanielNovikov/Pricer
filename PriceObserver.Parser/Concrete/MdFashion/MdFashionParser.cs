using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.MdFashion
{
    public class MdFashionParser : ParserProviderBase
    {
        public override ShopType ProviderType => ShopType.MdFashion;

        protected override int GetPrice(IList<IElement> elements)
        {
            var priceSpan = elements.First(e =>
                !string.IsNullOrEmpty(e.ClassName) &&
                e.ClassName.Contains("price_current"));

            var price = priceSpan.Attributes["data-price"].Value;
            
            return int.Parse(price);
        }

        protected override string GetTitle(IList<IElement> elements)
        {
            return elements
                .First(x =>
                    !string.IsNullOrEmpty(x.ClassName) &&
                    x.ClassName == "h1 product_title enhanced-product-name")
                .Text();
        }
    }
}