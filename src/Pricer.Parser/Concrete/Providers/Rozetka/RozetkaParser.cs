using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Rozetka;

public class RozetkaParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Rozetka;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = ".product-about__block .product-prices__big";
        
        var priceElement = document.QuerySelector<IHtmlParagraphElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(RozetkaParser)}:{nameof(GetPrice)}:Element");
        
        var price = priceElement.TextContent
            .Replace("₴", string.Empty)
            .Replace(" ", string.Empty)
            .Replace(" ", string.Empty);    
        
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[property='og:title']";
        
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(RozetkaParser)}:{nameof(GetTitle)}:Element");

        return titleElement.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "rz-gallery-main-content-image > img";
        
        var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(RozetkaParser)}:{nameof(GetImageUrl)}:Element");
        
        var imageSource = imageElement.Source ?? 
            throw new ArgumentNullException($"{nameof(RozetkaParser)}:{nameof(GetImageUrl)}:Element:Content");
        
        return new Uri(imageSource);
    }

    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = ".status-label.status-label--gray.ng-star-inserted";

        return document.QuerySelector<IHtmlParagraphElement>(selector) is null;
    }

    public CurrencyKey GetCurrency(IHtmlDocument document)
    {
        return CurrencyKey.UAH;
    }
}