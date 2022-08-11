using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.Estore;

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

        return titleElement.TextContent.TrimStart('\n');
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {  
        const string selector = "meta[property='og:image']";

        var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
            throw new ArgumentNullException($"{nameof(EstoreParser)}:{nameof(GetImageUrl)}:Element");

        var imageSource = imageElement.Content ??
            throw new ArgumentNullException($"{nameof(EstoreParser)}:{nameof(GetImageUrl)}:Element:Content");
        
        return new Uri(imageSource);
    }
    
    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "p.availability.out-of-stock";
        
        return document.QuerySelector<IHtmlParagraphElement>(selector) is null;
    }

    public CurrencyKey GetCurrency(IHtmlDocument document)
    {
        return CurrencyKey.UAH;
    }
}