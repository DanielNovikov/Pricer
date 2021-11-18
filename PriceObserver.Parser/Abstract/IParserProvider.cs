using AngleSharp.Html.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserProvider
    {
        ShopType ProviderType { get; } 

        ParsedItem Parse(IHtmlDocument document);
    }
}