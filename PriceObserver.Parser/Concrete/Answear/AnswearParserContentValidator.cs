using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract.Answear;

namespace PriceObserver.Parser.Concrete.Answear
{
    public class AnswearParserContentValidator : IAnswearParserContentValidator
    {
        public ContentValidatorResult Validate(IHtmlDocument htmlDocument)
        {
            var elements = htmlDocument.All.ToList();
            
            if (!PriceExistsOnPage(elements))
                return ContentValidatorResult.PriceDoesNotExist();

            return ContentValidatorResult.Success();
        }

        private bool PriceExistsOnPage(List<IElement> elements)
        {
            return elements
                .Any(e => 
                    e.TagName.ToLower() == "p" && 
                    !string.IsNullOrEmpty(e.ClassName) &&
                    e.ClassName.Contains("Price__currentPrice"));
        }
    }
}