using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract.Answear;

namespace PriceObserver.Parser.Concrete.Answear
{
    public class AnswearParser : IAnswearParser
    {
        public ParsedItem Parse(IHtmlDocument htmlDocument)
        {
            var elements = htmlDocument.All.ToList();

            return new ParsedItem
            {
                ShopType = ShopType.Answear,
                Price = GetPrice(elements)
            };
        }

        private int GetPrice(List<IElement> elements)
        {
            var priceSpan = elements.First(e =>
                !string.IsNullOrEmpty(e.ClassName) &&
                e.ClassName.Contains("Price__currentPrice"));

            var fullPrice = priceSpan.InnerHtml;

            var spaceIndex = fullPrice.IndexOf(' ');
            var price = fullPrice.Substring(0, spaceIndex);
            
            return int.Parse(price);
        }
    }
}