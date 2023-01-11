using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Models;

namespace Pricer.Parser.Abstract;

public interface IParserProviderService
{
    ParsedItem Parse(ShopKey providerKey, IHtmlDocument document);
}