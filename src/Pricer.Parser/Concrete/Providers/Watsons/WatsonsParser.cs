using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Watsons;

public class WatsonsParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Watsons;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "p.pdp-details__price";

        var priceElement = document.QuerySelector<IHtmlParagraphElement>(selector) ??
            throw new ArgumentNullException($"{nameof(WatsonsParser)}:{nameof(GetPrice)}:Element");

        var price = priceElement.TextContent
            .Replace(",", string.Empty)
            .Replace("грн", string.Empty)
            .Replace(" ", string.Empty)
            .Trim('\r', '\n');

        return int.Parse(price) / 100; 
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[property='og:title']";
        
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(WatsonsParser)}:{nameof(GetTitle)}:Element");

        return titleElement.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "meta[itemprop=image]";

        var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(WatsonsParser)}:{nameof(GetTitle)}:Element");
        
        var imageUrl = $"https://www.watsons.ua{imageElement.Content}";

        return new Uri(imageUrl);
    }
    
    public bool IsAvailable(IHtmlDocument document)
    {
        return true;
    }

    public CurrencyKey GetCurrency(IHtmlDocument document)
    {
        return CurrencyKey.UAH;
    }
}