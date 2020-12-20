using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
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
                Shop = ShopEnum.Intertop,
                Price = GetPrice(elements),
                IsItemOutOfStock = IsItemOutOfStock(elements)
            };
        }

        private int GetPrice(List<IElement> elements)
        {
            var priceSpan = elements.First(e => e.ClassName == "price-contain");
            
            var price = priceSpan.InnerHtml.Replace(" ", string.Empty);
            
            return int.Parse(price);
        }
        
        private bool IsItemOutOfStock(List<IElement> elements)
        {
            return elements.Any(e => e.ClassName == "prod-pay no-item");
        }
    }
}