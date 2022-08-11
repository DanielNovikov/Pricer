using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.Athletics;

public class AthleticsParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Athletics;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "meta[property='product:price:amount']";
        
        var priceElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(AthleticsParser)}:{nameof(GetPrice)}:Element");
        
        var price = priceElement.Content?.Replace(" ", string.Empty) ?? 
            throw new ArgumentNullException($"{nameof(AthleticsParser)}:{nameof(GetPrice)}:Element:Content");    
        
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[name=Keywords]";
        
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(AthleticsParser)}:{nameof(GetTitle)}:Element");

        return titleElement.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "div.popup-gallery__slider_viewport__inner > div:first-child > img";
        
        var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(AthleticsParser)}:{nameof(GetImageUrl)}:Element");
        
        var imageSource = imageElement.Source ?? 
            throw new ArgumentNullException($"{nameof(AthleticsParser)}:{nameof(GetImageUrl)}:Element:Content");
        
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