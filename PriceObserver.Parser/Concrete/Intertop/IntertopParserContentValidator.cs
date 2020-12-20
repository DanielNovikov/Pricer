using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Parser.Abstract.Intertop;

namespace PriceObserver.Parser.Concrete.Intertop
{
    public class IntertopParserContentValidator : IIntertopParserContentValidator
    {
        public void Validate(IHtmlDocument htmlDocument)
        {
            var elements = htmlDocument.All.ToList();
            
            if (!PriceExistsOnPage(elements))
                throw new Exception("There is no price on page");
        }

        private bool PriceExistsOnPage(List<IElement> elements)
        {
            return elements
                .Where(e => e.TagName.ToLower() == "span")
                .Any(e => e.ClassName == "price-contain");
        }
    }
}