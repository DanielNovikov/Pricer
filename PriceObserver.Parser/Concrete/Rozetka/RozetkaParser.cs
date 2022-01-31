using System;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Rozetka;

public class RozetkaParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Rozetka;
    
    public int GetPrice(IHtmlDocument document)
    {
        const string selector = ".product-about__block .product-prices__big";
        var price = document.QuerySelector<IHtmlParagraphElement>(selector);
        var priceText = price!.Text()
            .Replace("₴", string.Empty)
            .Replace(" ", string.Empty)
            .Replace(" ", string.Empty);    
        
        return int.Parse(priceText);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "meta[name=keywords]";
        var titleElement = document.QuerySelector<IHtmlMetaElement>(selector);

        return titleElement!.Content;
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "rz-gallery-main-content-image > img";
        var titleElement = document.QuerySelector<IHtmlImageElement>(selector);
        var imageSource = titleElement!.Source; 
        
        return new Uri(imageSource!);
    }
}