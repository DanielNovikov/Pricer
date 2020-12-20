using AngleSharp.Html.Dom;
using PriceObserver.Model.Parser;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserProviderService
    {
        public string Host { get; }

        public ParsedItem Parse(IHtmlDocument htmlDocument);
    }
}