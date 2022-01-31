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
        var price = document.QuerySelector<IHtmlSpanElement>(selector)!.TextContent;

        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[name=KeyWords]";
        return document.QuerySelector<IHtmlMetaElement>(selector)!.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "img[itemprop=image]";
        var source = document.QuerySelector<IHtmlImageElement>(selector)!.Source;
            
        return new Uri(source!);
    }
}