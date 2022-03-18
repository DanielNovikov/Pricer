using System;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Sportmaster;

public class SportmasterParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Sportmaster;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "meta[property='product:price:amount']";
        
        var priceElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(SportmasterParser)}:{nameof(GetPrice)}:Element");
        
        var price = priceElement.Content?.Replace(" ", string.Empty) ?? 
            throw new ArgumentNullException($"{nameof(SportmasterParser)}:{nameof(GetPrice)}:Element:Content");    
        
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[name=Keywords]";
        
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(SportmasterParser)}:{nameof(GetTitle)}:Element");

        return titleElement.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "div.popup-gallery__slider_viewport__inner > div:first-child > img";
        
        var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(SportmasterParser)}:{nameof(GetImageUrl)}:Element");
        
        var imageSource = imageElement.Source ?? 
            throw new ArgumentNullException($"{nameof(SportmasterParser)}:{nameof(GetImageUrl)}:Element:Content");
        
        return new Uri(imageSource);
    }
}