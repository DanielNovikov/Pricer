using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract.MdFashion;

namespace PriceObserver.Parser.Concrete.MdFashion
{
    public class MdFashionParserContentValidator : IMdFashionParserContentValidator
    {
        public ContentValidatorResult Validate(IHtmlDocument htmlDocument)
        {
            var elements = htmlDocument.All.ToList();
            
            if (!PriceExistsOnPage(elements))
                 ContentValidatorResult.Fail("На странице нет цены");

            return ContentValidatorResult.Success();
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