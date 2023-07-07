using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Miraton;

public class MiratonParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Miraton;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "span.price";
        
        var priceElement = document.QuerySelector<IHtmlSpanElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(MiratonParser)}:{nameof(GetPrice)}:Element");

        var price = priceElement.TextContent
            .Replace(" ", string.Empty)
            .Replace("грн", string.Empty);;
        
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string brandSelector = "div.product-info-brand > a";
        const string modelSelector = "h1.item-title";

        var brandElement = document.QuerySelector<IHtmlElement>(brandSelector) ?? 
            throw new ArgumentNullException($"{nameof(MiratonParser)}:{nameof(GetTitle)}:Element:Brand");

        var modelElement = document.QuerySelector<IHtmlHeadingElement>(modelSelector) ??
            throw new ArgumentNullException($"{nameof(MiratonParser)}:{nameof(GetTitle)}:Element:Model");

        return $"{brandElement.TextContent} - {modelElement.TextContent.Trim()}";
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "div.product-view-slider > div:first-child img";
        
        var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(MiratonParser)}:{nameof(GetImageUrl)}:Element");
        
        var relativeImageSource = imageElement.Source ?? 
            throw new ArgumentNullException($"{nameof(MiratonParser)}:{nameof(GetImageUrl)}:Element:Content");

        var imageSource = $"https://www.miraton.ua{relativeImageSource}";
        return new Uri(imageSource);
    }

    public bool IsAvailable(IHtmlDocument document)
    {
        return true;
    }

    public CurrencyKey GetCurrency(IHtmlDocument document)
    {
        return CurrencyKey.UAH;
    }
}