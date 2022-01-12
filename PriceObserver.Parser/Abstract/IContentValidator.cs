using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Parser.Abstract;

public interface IContentValidator
{
    ShopKey ProviderKey { get; }
        
    bool IsPriceExists(IHtmlDocument document);
}