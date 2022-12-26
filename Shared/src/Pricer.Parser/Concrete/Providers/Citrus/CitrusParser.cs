using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Citrus;

public class CitrusParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Citrus;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "div.price";
        
        var priceElement = document.QuerySelector<IHtmlDivElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(CitrusParser)}:{nameof(GetPrice)}:Element");
        
        var price = priceElement.GetAttribute("data-price") ??
            throw new ArgumentNullException($"{nameof(CitrusParser)}:{nameof(GetPrice)}:Element:Content");
        
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "h1.title-0-2-122";
        
        var titleElement = document.QuerySelector<IHtmlHeadingElement>(selector) ??
            throw new ArgumentNullException($"{nameof(CitrusParser)}:{nameof(GetTitle)}:Element");

        return titleElement.TextContent;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "meta[property='og:image']";
        
        var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(CitrusParser)}:{nameof(GetImageUrl)}:Element");
        
        var imageSource = imageElement.Content ??
            throw new ArgumentNullException($"{nameof(CitrusParser)}:{nameof(GetImageUrl)}:Element:Content"); 
        
        return new Uri(imageSource);
    }
    
    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "span.fz14.aic";

        var availabilityElement = document.QuerySelector<IHtmlSpanElement>(selector);
        if (availabilityElement is null)
            return true;

        return !availabilityElement.TextContent.Contains("Нет в наличии");
    }

    public CurrencyKey GetCurrency(IHtmlDocument document)
    {
        return CurrencyKey.UAH;
    }
}