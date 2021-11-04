using AngleSharp.Html.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Parser;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserProviderContentValidator
    {
        ShopType ProviderType { get; }
        
        ContentValidatorResult Validate(IHtmlDocument htmlDocument);
    }
}