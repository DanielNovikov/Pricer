using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserProvider
    {
        ShopKey ProviderType { get; } 

        ParsedItem Parse(IHtmlDocument document);
    }
}