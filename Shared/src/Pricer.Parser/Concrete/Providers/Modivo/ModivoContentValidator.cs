using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Modivo;

public class ModivoContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Modivo;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "meta[property='product:price:amount']";
        
        return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
    }
}