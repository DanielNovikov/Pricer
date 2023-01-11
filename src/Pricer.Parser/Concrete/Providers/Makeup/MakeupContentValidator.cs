using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Makeup;

public class MakeupContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Makeup;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "meta[property='og:image']";

        return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
    }
}