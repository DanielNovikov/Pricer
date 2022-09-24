using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.Telemart;

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
    
    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = ".b-product-mpr-info .b-i-product-available-gray";

        return document.QuerySelector<IHtmlDivElement>(selector) is null;
    }

    public CurrencyKey GetCurrency(IHtmlDocument document)
    {
        return CurrencyKey.UAH;
    }
}