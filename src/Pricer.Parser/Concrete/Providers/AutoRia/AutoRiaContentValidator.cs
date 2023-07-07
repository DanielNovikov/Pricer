using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.AutoRia;

public class AutoRiaContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.AutoRia;
    
    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "div.price_value";

        return document.QuerySelectorAll<IHtmlElement>(selector).Any();
    }
}