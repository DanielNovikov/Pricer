using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Stylus;

public class StylusParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Stylus;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "div.product-info-block div.regular-price";
        
        var priceElement = 
            document.QuerySelector<IHtmlDivElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(StylusParser)}:{nameof(GetPrice)}:Element");

        var priceText = priceElement
            .TextContent
            .Replace(" ", string.Empty)
            .Replace("грн", string.Empty);
        
        return int.Parse(priceText);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "h1.page-name";
        
        var titleElement = 
            document.QuerySelector<IHtmlHeadingElement>(selector) ??
            throw new ArgumentNullException($"{nameof(StylusParser)}:{nameof(GetTitle)}:Element");

        return titleElement.TextContent;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "meta[property='og:image']";
        
        var imageMetaElement =
            document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(StylusParser)}:{nameof(GetImageUrl)}:Element");
        
        var imageSource = imageMetaElement.Content; 
        
        return new Uri(imageSource!);
    }
}