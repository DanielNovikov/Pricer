using AngleSharp.Html.Dom;
using PriceObserver.Model.Parser;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserProvider
    {
        public ParsedItem Parse(IHtmlDocument htmlDocument);
    }
}