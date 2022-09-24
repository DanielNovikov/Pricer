using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Moyo;

public class MoyoContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Moyo;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "meta[itemprop=price]";

        return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
    }
}