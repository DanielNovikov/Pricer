using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Makeup;

public class MakeupParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Makeup;
        
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "span[itemprop=price]";
        
        var priceElement = document.QuerySelector<IHtmlSpanElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(MakeupParser)}:{nameof(GetPrice)}:Element");
        
        var price = priceElement.TextContent;

        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[name=KeyWords]";
        
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(MakeupParser)}:{nameof(GetTitle)}:Element");

        return titleElement.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "img[itemprop=image]";
        
        var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(MakeupParser)}:{nameof(GetImageUrl)}:Element");

        var imageSource = imageElement.Source ?? 
            throw new ArgumentNullException($"{nameof(MakeupParser)}:{nameof(GetImageUrl)}:Element:Content");
        
        return new Uri(imageSource);
    }
}