using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.DeeZee;

public class DeeZeeContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.DeeZee;
    
    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "p[data-type='product-price']";

        return document.QuerySelector<IHtmlParagraphElement>(selector) != null;
    }
}