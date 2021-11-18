using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Base
{
    public abstract class ParserProviderBase : IParserProvider
    {
        public abstract ShopType ProviderType { get; }

        public ParsedItem Parse(IHtmlDocument document)
        {
            var elements = document.All.ToList();

            return new ParsedItem
            {
                ShopType = ProviderType,
                Price = GetPrice(elements),
                Title = GetTitle(elements).Trim(' ', '\r', '\n'),
                ImageUrl = GetImageUrl(document)
            };
        }
        
        protected abstract int GetPrice(IList<IElement> elements);
        
        protected abstract string GetTitle(IList<IElement> elements);

        protected abstract Uri GetImageUrl(IHtmlDocument document);
    }
}