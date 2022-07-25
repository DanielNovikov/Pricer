using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.Moyo;

public class MoyoParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Moyo;
    
    public int GetPrice(IHtmlDocument document)
    {   
        const string selector = "meta[itemprop=price]";
        
        var priceElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(MoyoParser)}:{nameof(GetPrice)}:Element");

        var price = priceElement.Content ??
            throw new ArgumentNullException($"{nameof(MoyoParser)}:{nameof(GetPrice)}:Element:Content");;  
        
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[name=keywords]";
        
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(MoyoParser)}:{nameof(GetTitle)}:Element");

        return titleElement.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "div.product_image_item-wrap:first-child > a";
        
        var imageElement = document.QuerySelector<IHtmlElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(MoyoParser)}:{nameof(GetImageUrl)}:Element");
        
        var imageSource = imageElement.GetAttribute("href") ?? 
            throw new ArgumentNullException($"{nameof(MoyoParser)}:{nameof(GetImageUrl)}:Element:Content");
        
        return new Uri(imageSource);
    }
    
    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "div.noinstock-status";

        return document.QuerySelector<IHtmlDivElement>(selector) is null;
    }
}