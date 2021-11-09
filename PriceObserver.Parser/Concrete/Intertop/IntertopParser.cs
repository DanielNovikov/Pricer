using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Intertop
{
    public class IntertopParser : ParserProviderBase
    {
        public override ShopType ProviderType => ShopType.Intertop;

        protected override int GetPrice(IList<IElement> elements)
        {
            var priceSpan = elements.First(e =>
                !string.IsNullOrEmpty(e.ClassName) &&
                e.ClassName == "price-contain");
            
            var price = priceSpan.InnerHtml.Replace(" ", string.Empty);
            
            return int.Parse(price);
        }

        protected override string GetTitle(IList<IElement> elements)
        {
            var productName = elements
                .First(e =>
                    !string.IsNullOrEmpty(e.ClassName) &&
                    e.ClassName == "user-product-name")
                .Children;

            var type = productName[1].Text();
            var description = productName[0].Text(); 

            return $"{type} {description}";
        }

        protected override Uri GetImageUrl(IHtmlDocument document)
        {
            const string selector = "meta[property='og:image']";
            var meta = document.QuerySelector<IHtmlMetaElement>(selector);
            var imageUrl = $"https://intertop.ua/{meta.Content}";

            return new Uri(imageUrl);
        }
    }
}