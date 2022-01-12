using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Abstract;

public interface IParserProviderService
{
    ParsedItem Parse(ShopKey providerKey, IHtmlDocument document);
}