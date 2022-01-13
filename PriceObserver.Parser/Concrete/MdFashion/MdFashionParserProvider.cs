﻿using System;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.MdFashion;

public class MdFashionParserProvider : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.MdFashion;

    public int GetPrice(IHtmlDocument document)
    {
        var priceSpan = document.All.First(e =>
            !string.IsNullOrEmpty(e.ClassName) &&
            e.ClassName.Contains("price_current"));

        var price = priceSpan.Attributes["data-price"].Value;
            
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        return document.All
            .First(x =>
                !string.IsNullOrEmpty(x.ClassName) &&
                x.ClassName == "h1 product_title enhanced-product-name")
            .Text();
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = ".product_image > div[id=product0] > span > img";
        var image = document.QuerySelector<IHtmlImageElement>(selector);
        var imageUrl = image.Source;

        return new Uri(imageUrl);
    }
}