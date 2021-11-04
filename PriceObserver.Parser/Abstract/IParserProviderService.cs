using AngleSharp.Html.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Parser;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserProviderService
    {
        ParsedItemResult Parse(ShopType providerType, IHtmlDocument html);
    }
}