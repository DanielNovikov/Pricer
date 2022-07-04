﻿using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Citrus;

public class CitrusParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Citrus;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "div.price";
        
        var priceElement = document.QuerySelector<IHtmlDivElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(CitrusParser)}:{nameof(GetPrice)}:Element");
        
        var price = priceElement.GetAttribute("data-price") ??
            throw new ArgumentNullException($"{nameof(CitrusParser)}:{nameof(GetPrice)}:Element:Content");
        
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[property='og:title']";
        
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(CitrusParser)}:{nameof(GetTitle)}:Element");

        return titleElement.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "meta[property='og:image']";
        
        var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(CitrusParser)}:{nameof(GetImageUrl)}:Element");
        
        var imageSource = imageElement.Content ??
            throw new ArgumentNullException($"{nameof(CitrusParser)}:{nameof(GetImageUrl)}:Element:Content"); 
        
        return new Uri(imageSource);
    }
}