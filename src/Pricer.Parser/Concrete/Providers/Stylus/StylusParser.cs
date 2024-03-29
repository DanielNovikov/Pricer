﻿using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Stylus;

public class StylusParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Stylus;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "div.product-info-block div.regular-price";
        
        var priceElement = document.QuerySelector<IHtmlDivElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(StylusParser)}:{nameof(GetPrice)}:Element");

        var price = priceElement.TextContent
            .Replace(" ", string.Empty)
            .Replace("грн", string.Empty);
        
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "h1.page-name";
        
        var titleElement = document.QuerySelector<IHtmlHeadingElement>(selector) ??
            throw new ArgumentNullException($"{nameof(StylusParser)}:{nameof(GetTitle)}:Element");

        return titleElement.TextContent;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "meta[property='og:image']";
        
        var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(StylusParser)}:{nameof(GetImageUrl)}:Element");
        
        var imageSource = imageElement.Content ??
            throw new ArgumentNullException($"{nameof(StylusParser)}:{nameof(GetImageUrl)}:Element:Content"); 
        
        return new Uri(imageSource);
    }
    
    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "div.availability.not-available";
        
        return document.QuerySelector<IHtmlDivElement>(selector) is null;
    }

    public CurrencyKey GetCurrency(IHtmlDocument document)
    {
        return CurrencyKey.UAH;
    }
}