using System;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Parser.Abstract;

public interface IParserProvider
{
    ShopKey ProviderKey { get; }
    
    int GetPrice(IHtmlDocument document);
    
    string GetTitle(IHtmlDocument document);

    Uri GetImageUrl(IHtmlDocument document);

    bool IsAvailable(IHtmlDocument document);
}