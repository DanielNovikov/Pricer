using AngleSharp.Html.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserProviderService
    {
        ParsedItemResult Parse(ShopType providerType, IHtmlDocument html);
    }
}