using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Adidas
{
    public class AdidasParserContentValidator : ParserProviderContentValidatorBase
    {
        public override ShopType ProviderType => ShopType.Adidas;
        
        protected override bool IsPriceExists(IHtmlDocument document)
        {
            const string discountPriceSelector = "div.product__sidebar__inner > div > span.product__price--sale";
            const string fullPriceSelector = "div.product__sidebar__inner > div > span.product__price--first";

            return 
                document.QuerySelector<IHtmlSpanElement>(discountPriceSelector) is not null ||
                document.QuerySelector<IHtmlSpanElement>(fullPriceSelector) is not null;
        }
    }
}