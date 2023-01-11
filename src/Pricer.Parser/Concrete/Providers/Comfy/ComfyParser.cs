using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Comfy;

public class ComfyParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Comfy;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string discountPriceSelector = "meta[name='product:sale_price:amount']";
        const string fullPriceSelector = "meta[name='product:price:amount']";
        
        var priceElement = 
            document.QuerySelector<IHtmlMetaElement>(discountPriceSelector) ??
            document.QuerySelector<IHtmlMetaElement>(fullPriceSelector) ?? 
            throw new ArgumentNullException($"{nameof(ComfyParser)}:{nameof(GetPrice)}:Element");
        
        var price = priceElement.Content ??
            throw new ArgumentNullException($"{nameof(ComfyParser)}:{nameof(GetPrice)}:Element:Content");
        
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[name=keywords]";
        
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(ComfyParser)}:{nameof(GetTitle)}:Element");

        return titleElement.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "link[data-qmeta=mainImage]";
        
        var imageElement = document.QuerySelector<IHtmlLinkElement>(selector) ??
            throw new ArgumentNullException($"{nameof(ComfyParser)}:{nameof(GetImageUrl)}:Element");
        
        var imageSource = imageElement.Href ??
            throw new ArgumentNullException($"{nameof(ComfyParser)}:{nameof(GetImageUrl)}:Element:Content"); 
        
        return new Uri(imageSource);
    }
    
    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "div.price__out-of-stock";

        return document.QuerySelector<IHtmlDivElement>(selector) is null;
    }

    public CurrencyKey GetCurrency(IHtmlDocument document)
    {
        return CurrencyKey.UAH;
    }
}