using System;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Brocard
{
    public class BrocardParser : ParserProviderBase
    {
        public override ShopType ProviderType => ShopType.Brocard;

        protected override int GetPrice(IHtmlDocument document)
        {
            const string selector = ".price-format > .price";
            var spanText = document.QuerySelector<IHtmlSpanElement>(selector)!.Text();
            
            var price = spanText.Replace(" ", string.Empty);
            price = price[..price.IndexOf(',')];

            return int.Parse(price);
        }

        protected override string GetTitle(IHtmlDocument document)
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

        protected override Uri GetImageUrl(IHtmlDocument document)
        {
            const string selector = "meta[property='og:image']";
            var imageUrl = document.QuerySelector<IHtmlMetaElement>(selector)!.Content;
            
            return new Uri(imageUrl!);
        }
    }
}