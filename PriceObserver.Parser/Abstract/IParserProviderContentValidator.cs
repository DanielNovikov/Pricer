using AngleSharp.Html.Dom;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserProviderContentValidator
    {
        void Validate(IHtmlDocument htmlDocument);
    }
}