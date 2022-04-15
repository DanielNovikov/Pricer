using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Brocard;

public class BrocardParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Brocard;

    public int GetPrice(IHtmlDocument document)
    {
        const string selector = ".price-format > .price";
        
        var priceElement = document.QuerySelector<IHtmlSpanElement>(selector) ??
            throw new ArgumentNullException($"{nameof(BrocardParser)}:{nameof(GetPrice)}:Element");
        
        var price = priceElement.TextContent;

        var priceWithoutSpaces = price.Replace(" ", string.Empty);
        var formattedPrice = priceWithoutSpaces[..priceWithoutSpaces.IndexOf(',')];
        
        return int.Parse(formattedPrice);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "ul[class$=itemsCritical] li:last-child span";

        var titleElement = document.QuerySelector<IHtmlSpanElement>(selector) ??
            throw new ArgumentNullException($"{nameof(BrocardParser)}:{nameof(GetTitle)}:Element");

        return titleElement.TextContent.Trim(' ', '\r', '\n');
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "meta[property='og:image']";
        
        var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(BrocardParser)}:{nameof(GetImageUrl)}:Element");

        var imageSource = imageElement.Content ?? 
            throw new ArgumentNullException($"{nameof(BrocardParser)}:{nameof(GetImageUrl)}:Element:Content");
        
        return new Uri(imageSource);
    }
}