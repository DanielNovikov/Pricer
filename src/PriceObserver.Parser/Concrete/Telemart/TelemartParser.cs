using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Telemart;

public class TelemartParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Telemart;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "meta[itemprop=price]";

        var priceElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(TelemartParser)}:{nameof(GetPrice)}:Element");

        var price = priceElement.Content ??
            throw new ArgumentNullException($"{nameof(TelemartParser)}:{nameof(GetPrice)}:Element:Content");
        
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[property='og:title']";
        
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(TelemartParser)}:{nameof(GetTitle)}:Element");

        return titleElement.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "meta[property='og:image']";

        var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(TelemartParser)}:{nameof(GetImageUrl)}:Element");

        var imageUrl = imageElement.Content ??
            throw new ArgumentNullException($"{nameof(TelemartParser)}:{nameof(GetImageUrl)}:Element:Content");
        
        return new Uri(imageUrl);
    }
}