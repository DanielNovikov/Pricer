using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.Adidas;

public class AdidasParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Adidas;
        
    public int GetPrice(IHtmlDocument document)
    {
        const string discountPriceSelector = "div.product__sidebar__inner > div > span.product__price--sale";
        const string fullPriceSelector = "div.product__sidebar__inner > div > span.product__price--first";

        var priceElement = 
            document.QuerySelector<IHtmlSpanElement>(discountPriceSelector) ??
            document.QuerySelector<IHtmlSpanElement>(fullPriceSelector) ??
            throw new ArgumentNullException($"{nameof(AdidasParser)}:{nameof(GetPrice)}:Element");

        var price = priceElement.TextContent;
        var formattedPrice = price
            .Substring(0, price.IndexOf("грн", StringComparison.Ordinal))
            .Replace(" ", string.Empty);

        return int.Parse(formattedPrice);
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string selector = ".product__sidebar__inner > .common-text.product__title > span";

        var titleElement = document.QuerySelector<IHtmlSpanElement>(selector) ??
            throw new ArgumentNullException($"{nameof(AdidasParser)}:{nameof(GetTitle)}:Element");

        return titleElement.TextContent.Trim(' ', '\n');
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = ".slick-current.slick-slide.slick-active > div> div> div> div> picture > img";
        
        var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ??
            throw new ArgumentNullException($"{nameof(AdidasParser)}:{nameof(GetImageUrl)}:Element");

        var imageSource = imageElement.Source ??
            throw new ArgumentNullException($"{nameof(AdidasParser)}:{nameof(GetImageUrl)}:Element:Content");

        return new Uri(imageSource);
    }
}