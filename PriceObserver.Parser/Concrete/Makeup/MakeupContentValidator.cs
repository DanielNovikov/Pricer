using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Makeup;

public class MakeupContentValidator : ContentValidatorBase
{
    public override ShopKey ProviderType => ShopKey.Makeup;
        
    protected override bool IsPriceExists(IHtmlDocument document)
    {
        const string selector = "span[itemprop=price]";
        return document.QuerySelector<IHtmlSpanElement>(selector) != null;
    }
}