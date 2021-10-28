using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract.Intertop;

namespace PriceObserver.Parser.Concrete.Intertop
{
    public class IntertopParserContentValidator : IIntertopParserContentValidator
    {
        public ContentValidatorResult Validate(IHtmlDocument htmlDocument)
        {
            var elements = htmlDocument.All.ToList();
            
            if (!PriceExistsOnPage(elements))
                return ContentValidatorResult.Fail("На странице нет цены");

            return ContentValidatorResult.Success();
        }

        private bool PriceExistsOnPage(List<IElement> elements)
        {
            return elements
                .Any(e => 
                    e.TagName.ToLower() == "span" && 
                    !string.IsNullOrEmpty(e.ClassName) &&
                    e.ClassName == "price-contain");
        }
    }
}