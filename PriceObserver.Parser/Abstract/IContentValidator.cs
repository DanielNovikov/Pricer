using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Parser.Abstract;

public interface IContentValidator
{
    ShopKey ProviderKey { get; }

    bool IsAvailable(IHtmlDocument document);
    
    bool IsPriceExists(IHtmlDocument document);
}