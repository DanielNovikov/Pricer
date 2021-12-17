using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Brocard
{
    public class BrocardParserContentValidator : ParserProviderContentValidatorBase
    {
        public override ShopKey ProviderType => ShopKey.Brocard;
        
        protected override bool IsPriceExists(IHtmlDocument document)
        {
            const string selector = ".price-format > .price";
            
            return document.QuerySelector(selector) is not null;
        }
    }
}