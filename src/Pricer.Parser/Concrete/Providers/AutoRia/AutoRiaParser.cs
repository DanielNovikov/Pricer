using System;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.AutoRia;

public class AutoRiaParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.AutoRia;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string divSelector = "div.price_value:first-child";
        const string strongSelector = "div.price_value > strong";
        
        var priceElement = 
            document.QuerySelector<IHtmlElement>(strongSelector) ??
            document.QuerySelector<IHtmlElement>(divSelector) ??
            throw new ArgumentNullException($"{nameof(AutoRiaParser)}:{nameof(GetPrice)}:Element");;

        var price = Regex.Replace(priceElement.TextContent, @"\D", string.Empty);
        
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "h1.head, h1.auto-head_title";
        
        var titleElement = document.QuerySelector<IHtmlHeadingElement>(selector) ??
            throw new ArgumentNullException($"{nameof(AutoRiaParser)}:{nameof(GetTitle)}:Element");

        return titleElement.TextContent;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "div.image-gallery-slides > div:first-child img, div.carousel-inner > div:first-child img";
        
        var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ??
            throw new ArgumentNullException($"{nameof(AutoRiaParser)}:{nameof(GetImageUrl)}:Element");

        var imageSource = imageElement.Source ??
            throw new ArgumentNullException($"{nameof(AutoRiaParser)}:{nameof(GetImageUrl)}:Element:Content");

        return new Uri(imageSource);
    }

    public bool IsAvailable(IHtmlDocument document)
    {
        return true;
    }

    public CurrencyKey GetCurrency(IHtmlDocument document)
    {
        const string divSelector = "div.price_value:first-child";
        const string strongSelector = "div.price_value > strong";
        
        var currencyElement = 
            document.QuerySelector<IHtmlElement>(strongSelector) ??
            document.QuerySelector<IHtmlElement>(divSelector) ??
            throw new ArgumentNullException($"{nameof(AutoRiaParser)}:{nameof(GetCurrency)}:Element");;

        var currency = currencyElement.TextContent;
        
        if (currency.EndsWith("€")) return CurrencyKey.EUR;
        if (currency.EndsWith("$")) return CurrencyKey.USD;
        return CurrencyKey.UAH;
    }
}