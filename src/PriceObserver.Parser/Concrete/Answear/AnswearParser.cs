using System;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Answear;

public class AnswearParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Answear;

    public int GetPrice(IHtmlDocument document)
    {
        const string selector = "div[class*=container-fluid] p[class^=Price__currentPrice_]";

        var priceElement = document.QuerySelector<IHtmlParagraphElement>(selector) ??
            throw new ArgumentNullException($"{nameof(AnswearParser)}:{nameof(GetPrice)}:Element");

        var price = priceElement.TextContent;

        var spaceIndex = price.IndexOf(' ');
        var formattedPrice = price.Substring(0, spaceIndex);
            
        return int.Parse(formattedPrice);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = "div[class^=ProductCard__productNameAndLogo] h1 span";

        var titleElements = document.QuerySelectorAll<IHtmlSpanElement>(selector);

        return titleElements
            .Select(x => x.TextContent)
            .Aggregate((x, y) => x + (x.EndsWith(' ') ? string.Empty : " ") + y);
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "div[class*=slick-current] div[class*=cardMedia] img";
        
        var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ??
            throw new ArgumentNullException($"{nameof(AnswearParser)}:{nameof(GetImageUrl)}:Element");
        
        var imageSource = imageElement.Source ??
            throw new ArgumentNullException($"{nameof(AnswearParser)}:{nameof(GetImageUrl)}:Element:Content");

        return new Uri(imageSource);
    }
}