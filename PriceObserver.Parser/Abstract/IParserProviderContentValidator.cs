using AngleSharp.Html.Dom;
using PriceObserver.Model.Parser;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserProviderContentValidator
    {
        ContentValidatorResult Validate(IHtmlDocument htmlDocument);
    }
}