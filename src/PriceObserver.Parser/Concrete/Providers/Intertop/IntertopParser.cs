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
        const string selector = "span[class=price-contain]";

        var priceElement = document.QuerySelector<IHtmlSpanElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(IntertopParser)}:{nameof(GetPrice)}:Element");
            
        var price = priceElement.TextContent;
        var formattedPrice = price.Replace(" ", string.Empty);
            
        return int.Parse(formattedPrice);
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
}