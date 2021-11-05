using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Parser;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Base
{
    public abstract class ParserProviderBase : IParserProvider
    {
        public abstract ShopType ProviderType { get; }

        public ParsedItem Parse(IHtmlDocument htmlDocument)
        {
            var elements = htmlDocument.All.ToList();

            return new ParsedItem
            {
                ShopType = ProviderType,
                Price = GetPrice(elements),
                Title = GetTitle(elements).Trim(' ', '\r', '\n')
            };
        }
        
        protected abstract int GetPrice(IList<IElement> elements);
        
        protected abstract string GetTitle(IList<IElement> elements);
    }
}