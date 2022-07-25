using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.MdFashion;

public class MdFashionParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.MdFashion;

    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "span[class^=price_current]";
        
        var priceElement = document.QuerySelector<IHtmlSpanElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(MdFashionParser)}:{nameof(GetPrice)}:Element");

        var price = priceElement.GetAttribute("data-price") ?? 
            throw new ArgumentNullException($"{nameof(MdFashionParser)}:{nameof(GetPrice)}:Element:Content");
            
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[property='og:title']";

        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(MdFashionParser)}:{nameof(GetTitle)}:Element");

        var title = titleElement.Content ?? 
            throw new ArgumentNullException($"{nameof(MdFashionParser)}:{nameof(GetTitle)}:Element:Content");
        
        var shopNameIndex = title.LastIndexOf("MD-Fashion", StringComparison.Ordinal);

        return title[..(shopNameIndex - 3)];
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "div[id=product0] img";
        
        var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(MdFashionParser)}:{nameof(GetImageUrl)}:Element");

        var imageSource = imageElement.Source ?? 
            throw new ArgumentNullException($"{nameof(MdFashionParser)}:{nameof(GetImageUrl)}:Element:Content");

        return new Uri(imageSource);
    }

    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "div[data-status=not-available]";

        return document.QuerySelector<IHtmlDivElement>(selector) is null;
    }
}