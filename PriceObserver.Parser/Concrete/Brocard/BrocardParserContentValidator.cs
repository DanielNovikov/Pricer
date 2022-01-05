using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Brocard;

public class BrocardParserContentValidator : ParserProviderContentValidatorBase
{
    public override ShopKey ProviderType => ShopKey.Brocard;
        
    protected override bool IsPriceExists(IHtmlDocument document)
    {
        const string selector = ".price-format > .price";
        
        var span = document.QuerySelector<IHtmlSpanElement>(selector);
        if (span == null)
            return false;
        
        var spanText = span.Text();

        var price = spanText.Replace(" ", string.Empty);
        price = price[..price.IndexOf(',')];
        
        var parsedPrice = int.Parse(price);
        return parsedPrice > 0;
    }
}