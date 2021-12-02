using AngleSharp.Html.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Brocard
{
    public class BrocardParserContentValidator : ParserProviderContentValidatorBase
    {
        public override ShopType ProviderType => ShopType.Brocard;
        
        protected override bool IsPriceExists(IHtmlDocument document)
        {
            const string selector = ".price-format > .price";
            
            return document.QuerySelector(selector) is not null;
        }
    }
}