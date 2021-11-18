using AngleSharp.Html.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserProviderContentValidator
    {
        ShopType ProviderType { get; }
        
        ContentValidatorResult Validate(IHtmlDocument htmlDocument);
    }
}