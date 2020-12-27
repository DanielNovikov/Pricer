using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Parser.Abstract.MdFashion;

namespace PriceObserver.Parser.Concrete.MdFashion
{
    public class MdFashionParserContentValidator : IMdFashionParserContentValidator
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
                .Any(e => 
                    e.TagName.ToLower() == "span" &&
                    !string.IsNullOrEmpty(e.ClassName) &&
                    e.ClassName.Contains("price_current"));
        }
    }
}