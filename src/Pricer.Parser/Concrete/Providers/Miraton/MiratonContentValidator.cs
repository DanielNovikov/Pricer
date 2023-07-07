using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Miraton;

public class MiratonContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Miraton;
    
    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "span.price";

        return document.QuerySelector<IHtmlSpanElement>(selector) != null;
    }
}