using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract.Intertop;

namespace PriceObserver.Parser.Concrete.Intertop
{
    public class IntertopParser : IIntertopParser
    {
        public ParsedItem Parse(IHtmlDocument htmlDocument)
        {
            var elements = htmlDocument.All.ToList();

            return new ParsedItem
            {
                ShopType = ShopType.Intertop,
                Price = GetPrice(elements),
                Title = GetTitle(elements)
            };
        }

        private int GetPrice(IList<IElement> elements)
        {
            var priceSpan = elements.First(e =>
                !string.IsNullOrEmpty(e.ClassName) &&
                e.ClassName == "price-contain");
            
            var price = priceSpan.InnerHtml.Replace(" ", string.Empty);
            
            return int.Parse(price);
        }

        private string GetTitle(IList<IElement> elements)
        {
            var productName = elements
                .First(e =>
                    !string.IsNullOrEmpty(e.ClassName) &&
                    e.ClassName == "user-product-name")
                .Children;

            var type = productName[1].Text();
            var description = productName[0].Text(); 

            return $"{type} {description}";
        }
    }
}