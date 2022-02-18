using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Estore;

public class EstoreParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Estore;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "meta[itemprop=price]";
        
        var priceElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(EstoreParser)}:{nameof(GetPrice)}:Element");

        var price = priceElement.Content ?? 
            throw new ArgumentNullException($"{nameof(EstoreParser)}:{nameof(GetPrice)}:ElementContent");
        
        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "h1[itemprop=name]";

        var titleElement = document.QuerySelector<IHtmlHeadingElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(EstoreParser)}:{nameof(GetTitle)}:Element");

        return titleElement.TextContent;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {  
        const string selector = "meta[property='og:image']";

        var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(EstoreParser)}:{nameof(GetImageUrl)}:Element");

        var imageSource = imageElement.Content ??
            throw new ArgumentNullException($"{nameof(EstoreParser)}:{nameof(GetImageUrl)}:ElementContent");
        
        return new Uri(imageSource);
    }
}