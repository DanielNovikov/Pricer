using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract.MdFashion;

namespace PriceObserver.Parser.Concrete.MdFashion
{
    public class MdFashionParser : IMdFashionParser
    {
        public ParsedItem Parse(IHtmlDocument htmlDocument)
        {
            var elements = htmlDocument.All.ToList();

            return new ParsedItem
            {
                ShopType = ShopType.MdFashion,
                Price = GetPrice(elements),
                Title = GetTitle(elements)
            };
        }

        private int GetPrice(IList<IElement> elements)
        {
            var priceSpan = elements.First(e =>
                !string.IsNullOrEmpty(e.ClassName) &&
                e.ClassName.Contains("price_current"));

            var price = priceSpan.Attributes["data-price"].Value;
            
            return int.Parse(price);
        }

        private string GetTitle(IList<IElement> elements)
        {
            return elements
                .First(x =>
                    !string.IsNullOrEmpty(x.ClassName) &&
                    x.ClassName == "h1 product_title enhanced-product-name")
                .Text()
                .TrimStart('\r', '\n');
        }
    }
}