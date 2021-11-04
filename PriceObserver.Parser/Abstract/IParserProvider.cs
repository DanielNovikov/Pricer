using AngleSharp.Html.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Parser;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserProvider
    {
        ShopType ProviderType { get; } 

        ParsedItem Parse(IHtmlDocument htmlDocument);
    }
}