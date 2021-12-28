using System;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Base;

public abstract class ParserProviderBase : IParserProvider
{
    public abstract ShopKey ProviderType { get; }

    public ParsedItem Parse(IHtmlDocument document)
    {
        return new ParsedItem(
            ProviderType,
            GetPrice(document),
            GetTitle(document).Trim(' ', '\r', '\n'),
            GetImageUrl(document));
    }
        
    protected abstract int GetPrice(IHtmlDocument document);
        
    protected abstract string GetTitle(IHtmlDocument document);

    protected abstract Uri GetImageUrl(IHtmlDocument document);
}