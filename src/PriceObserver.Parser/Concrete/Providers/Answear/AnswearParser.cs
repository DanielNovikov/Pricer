using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;
using System.Linq;

namespace PriceObserver.Parser.Concrete.Providers.Answear;

public class AnswearParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Answear;

    public int GetPrice(IHtmlDocument document)
    {
        const string discountPriceSelector = "div[class^=ProductCard] div[class^=Price__salePrice]";
        const string fullPriceSelector = "div[class^=ProductCard] div[class^=Price__price]";

        var priceElement = 
            document.QuerySelector<IHtmlDivElement>(discountPriceSelector) ??
            document.QuerySelector<IHtmlDivElement>(fullPriceSelector) ??
            throw new ArgumentNullException($"{nameof(AnswearParser)}:{nameof(GetPrice)}:Element");

        var price = priceElement.TextContent;
        var formattedPrice = price
            .Substring(0, price.IndexOf("грн", StringComparison.Ordinal))
            .Replace(" ", string.Empty);

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