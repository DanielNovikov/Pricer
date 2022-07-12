using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.Rozetka;

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
        const string selector = "meta[name=keywords]";
        
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
}