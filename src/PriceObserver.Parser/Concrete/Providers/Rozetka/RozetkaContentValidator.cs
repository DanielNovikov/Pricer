using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Rozetka;

public class RozetkaContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Rozetka;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = ".product-about__block .product-prices__big";
        
        return document.QuerySelector<IHtmlParagraphElement>(selector) is not null;
    }
}