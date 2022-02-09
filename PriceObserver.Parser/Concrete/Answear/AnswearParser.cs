﻿using System;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Answear;

public class AnswearParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Answear;

    public int GetPrice(IHtmlDocument document)
    {
        var elements = document.All;
            
        var priceSpan = elements.First(e =>
            !string.IsNullOrEmpty(e.ClassName) &&
            e.ClassName.Contains("Price__currentPrice"));

        var fullPrice = priceSpan.InnerHtml;

        var spaceIndex = fullPrice.IndexOf(' ');
        var price = fullPrice.Substring(0, spaceIndex);
            
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        return document.All
            .First(x =>
                !string.IsNullOrEmpty(x.ClassName) &&
                x.ClassName.Contains("productNameAndLogo"))
            .Children
            .First(x => x.TagName.ToLower() == "h1")
            .Text();
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = ".slick-current > div> .cardMedia >div > picture > img";
        var image = document.QuerySelector<IHtmlImageElement>(selector);
        var imageUrl = image.Source;

        return new Uri(imageUrl);
    }
}