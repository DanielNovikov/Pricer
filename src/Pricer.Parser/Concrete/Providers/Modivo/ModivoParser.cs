using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Modivo;

public class ModivoParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Modivo;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "meta[property='product:price:amount']";
        
        var priceElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(ModivoParser)}:{nameof(GetPrice)}:Element");
        
        var price = priceElement.Content ?? 
            throw new ArgumentNullException($"{nameof(ModivoParser)}:{nameof(GetPrice)}:Element:Content");

        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[name=title]";
        
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector)  ?? 
            throw new ArgumentNullException($"{nameof(ModivoParser)}:{nameof(GetTitle)}:Element");

        return titleElement.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "meta[property='og:image']";
        
        var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(ModivoParser)}:{nameof(GetImageUrl)}:Element");
        
        var imageSource = imageElement.Content ?? 
            throw new ArgumentNullException($"{nameof(ModivoParser)}:{nameof(GetImageUrl)}:Element:Content");
        
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