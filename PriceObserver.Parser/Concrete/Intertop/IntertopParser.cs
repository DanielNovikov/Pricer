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
                Price = GetPrice(elements)
            };
        }

        private int GetPrice(List<IElement> elements)
        {
            var priceSpan = elements.First(e =>
                !string.IsNullOrEmpty(e.ClassName) &&
                e.ClassName == "price-contain");
            
            var price = priceSpan.InnerHtml.Replace(" ", string.Empty);
            
            return int.Parse(price);
        }
    }
}