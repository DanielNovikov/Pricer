using System;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Parser.Abstract;

public interface IParserProvider
{
    ShopKey ProviderKey { get; }
    
    int GetPrice(IHtmlDocument document);
    
    string GetTitle(IHtmlDocument document);

    Uri GetImageUrl(IHtmlDocument document);

    bool IsAvailable(IHtmlDocument document);

    CurrencyKey GetCurrency(IHtmlDocument document);
}