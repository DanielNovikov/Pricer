using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Makeup
{
    public class MakeupParserContentValidator : ParserProviderContentValidatorBase
    {
        public override ShopType ProviderType => ShopType.Makeup;
        
        protected override bool IsPriceExists(IHtmlDocument document)
        {
            const string selector = "span[itemprop=price]";
            return document.QuerySelector<IHtmlSpanElement>(selector) != null;
        }
    }
}