using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Modivo;

public class ModivoParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Modivo;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "meta[property='product:price:amount']";
        var priceElement = document.QuerySelector<IHtmlMetaElement>(selector);
        var price = priceElement!.Content;

        return int.Parse(price!);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[name=title]";
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector);

        return titleElement!.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "meta[property='og:image']";
        var imageElement = document.QuerySelector<IHtmlMetaElement>(selector);
        var imageSrc = imageElement!.Content;
        
        return new Uri(imageSrc!);
    }
}