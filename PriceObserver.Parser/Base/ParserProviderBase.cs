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
            return new ParsedItem
            {
                ShopType = ProviderType,
                Price = GetPrice(document),
                Title = GetTitle(document).Trim(' ', '\r', '\n'),
                ImageUrl = GetImageUrl(document)
            };
        }
        
        protected abstract int GetPrice(IHtmlDocument document);
        
        protected abstract string GetTitle(IHtmlDocument document);

        protected abstract Uri GetImageUrl(IHtmlDocument document);
    }
}