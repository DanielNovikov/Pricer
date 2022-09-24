using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.Farfetch;

public class FarfetchParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Farfetch;
        
    public int GetPrice(IHtmlDocument document)
    {
        const string discountPriceSelector = "strong[data-tstid=priceInfo-onsale]";
        const string fullPriceSelector = "span[data-tstid=priceInfo-original]";

        var priceElement = 
            document.QuerySelector<IHtmlElement>(discountPriceSelector) ??
            document.QuerySelector<IHtmlElement>(fullPriceSelector) ?? 
            throw new ArgumentNullException($"{nameof(FarfetchParser)}:{nameof(GetPrice)}:Element:Content");

        var price = priceElement.TextContent;
        var formattedPrice = price
            .Substring(0, price.IndexOf(' '))
            .Replace(" ", string.Empty);

        return int.Parse(formattedPrice);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[property='og:title']";
        
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
            throw new ArgumentNullException($"{nameof(FarfetchParser)}:{nameof(GetTitle)}:Element");

        var title = titleElement.Content ??
            throw new ArgumentNullException($"{nameof(FarfetchParser)}:{nameof(GetTitle)}:Element:Content");
        
        return title[..(title.LastIndexOf('-') - 1)];
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "img[data-test=imagery-img0]";
        
        var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ??
            throw new ArgumentNullException($"{nameof(FarfetchParser)}:{nameof(GetImageUrl)}:Element");

        var imageSource = imageElement.Source ??
            throw new ArgumentNullException($"{nameof(FarfetchParser)}:{nameof(GetImageUrl)}:Element:Content");
        
        return new Uri(imageSource);
    }

    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "div[data-tstid=outOfStock]";

        return document.QuerySelector<IHtmlDivElement>(selector) is null;
    }

    public CurrencyKey GetCurrency(IHtmlDocument document)
    {
        return CurrencyKey.UAH;
    }
}