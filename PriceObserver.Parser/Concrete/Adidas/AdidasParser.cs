using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Adidas;

public class AdidasParser : ParserProviderBase
{
    public override ShopKey ProviderType => ShopKey.Adidas;
        
    protected override int GetPrice(IHtmlDocument document)
    {
        const string discountPriceSelector = "div.product__sidebar__inner > div > span.product__price--sale";
        const string fullPriceSelector = "div.product__sidebar__inner > div > span.product__price--first";

        var priceElement = 
            document.QuerySelector<IHtmlSpanElement>(discountPriceSelector) ??
            document.QuerySelector<IHtmlSpanElement>(fullPriceSelector);

        var priceString = priceElement!.Text();
        var formattedPriceString = priceString
            .Substring(0, priceString.IndexOf("грн", StringComparison.Ordinal))
            .Replace(" ", string.Empty);

        return int.Parse(formattedPriceString);
    }

    protected override string GetTitle(IHtmlDocument document)
    {
        const string selector = ".product__sidebar__inner > .common-text.product__title > span";
        return document.QuerySelector<IHtmlSpanElement>(selector)!.TextContent;
    }

    protected override Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = ".slick-current.slick-slide.slick-active > div> div> div> div> picture > img";
        var source = document.QuerySelector<IHtmlImageElement>(selector)!.Source;

        return new Uri(source!);
    }
}