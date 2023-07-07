using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.DeeZee;

public class DeeZeeParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.DeeZee;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "p[data-type='product-price']";
        
        var priceElement = document.QuerySelector<IHtmlParagraphElement>(selector) ?? 
             throw new ArgumentNullException($"{nameof(DeeZeeParser)}:{nameof(GetPrice)}:Element");

        var price = priceElement.TextContent.Replace("грн", string.Empty);
         
         return (int)double.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "span[itemprop='name']";
        
        var modelElement = document.QuerySelector<IHtmlSpanElement>(selector) ??
            throw new ArgumentNullException($"{nameof(DeeZeeParser)}:{nameof(GetTitle)}:Element:Model");

        return modelElement.TextContent;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "img[itemprop=image]";

        var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(DeeZeeParser)}:{nameof(GetImageUrl)}:Element");

        var imageSource = imageElement.Source ??
            throw new ArgumentNullException($"{nameof(DeeZeeParser)}:{nameof(GetImageUrl)}:Element:Content");
        
        return new Uri(imageSource);
    }

    public bool IsAvailable(IHtmlDocument document)
    {
        return true;
    }

    public CurrencyKey GetCurrency(IHtmlDocument document)
    {
        const string selector = "p[data-type='product-price']";
        
        var priceElement = document.QuerySelector<IHtmlParagraphElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(DeeZeeParser)}:{nameof(GetPrice)}:Element");

        return priceElement.TextContent.Contains("грн") ? CurrencyKey.UAH : CurrencyKey.EUR;
    }
}