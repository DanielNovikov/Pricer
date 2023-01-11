using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;

namespace Pricer.Parser.Abstract;

public interface IContentValidator
{
    ShopKey ProviderKey { get; }
    
    bool HasItemInfo(IHtmlDocument document);
}