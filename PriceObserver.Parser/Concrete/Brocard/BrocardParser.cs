using System;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Brocard;

public class BrocardParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Brocard;

    public int GetPrice(IHtmlDocument document)
    {
        const string selector = ".price-format > .price";
        var spanText = document.QuerySelector<IHtmlSpanElement>(selector)!.Text();
            
        var price = spanText.Replace(" ", string.Empty);
        price = price[..price.IndexOf(',')];

        return int.Parse(price);
    }

    public string GetTitle(IHtmlDocument document)
    {
        var productName = document.All
            .First(e =>
                !string.IsNullOrEmpty(e.ClassName) &&
                e.ClassName.Contains("itemsCritical") &&
                e.TagName == "UL")
            .Children
            .Last()
            .Children
            .First();

        return productName.Text();
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "meta[property='og:image']";
        var imageUrl = document.QuerySelector<IHtmlMetaElement>(selector)!.Content;
            
        return new Uri(imageUrl!);
    }
}