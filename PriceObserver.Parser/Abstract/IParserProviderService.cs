using AngleSharp.Html.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Parser;

namespace PriceObserver.Parser.Abstract
{
    public interface IParserProviderService
    {
        public ShopType ProviderType { get; }

        public ParsedItemResult Parse(IHtmlDocument htmlDocument);
    }
}