using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.Intertop;

public class IntertopParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Intertop;

    public int GetPrice(IHtmlDocument document)
    {
        const string elementSelector = "span[class=price-contain]";

        var priceElement = document.QuerySelector<IHtmlSpanElement>(elementSelector);
        if (priceElement != null)
        {
            var price = priceElement.TextContent;
            var formattedPrice = price.Replace(" ", string.Empty);

            return int.Parse(formattedPrice);
        }
        
        const string scriptSelector = "main script[type='application/ld+json']";
        var scriptElement = document.QuerySelector<IHtmlScriptElement>(scriptSelector) ??
            throw new ArgumentNullException($"{nameof(IntertopParser)}:{nameof(GetPrice)}:Script");

        var scriptContent = scriptElement.TextContent;
        var pricePropertyStartIndex = scriptContent.IndexOf("\"price\":", StringComparison.Ordinal);
        var pricePropertyEndIndex = scriptContent.IndexOf(",", pricePropertyStartIndex, StringComparison.Ordinal);
        
        var priceString = scriptContent.Substring(
            pricePropertyStartIndex + 8,
            pricePropertyEndIndex - pricePropertyStartIndex - 8);

        return int.Parse(priceString);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[property='og:title']";
        
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(IntertopParser)}:{nameof(GetTitle)}:Element");

        return titleElement.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "meta[property='og:image']";
        
        var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(IntertopParser)}:{nameof(GetImageUrl)}:Element");
        
        var imageUrl = $"https://intertop.ua{imageElement.Content}";

        return new Uri(imageUrl);
    }

    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "a[id='report-availability']";

        return document.QuerySelector<IHtmlElement>(selector) is null;
    }
}