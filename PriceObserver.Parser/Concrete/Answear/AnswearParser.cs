﻿using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Answear
{
    public class AnswearParser : ParserProviderBase
    {
        public override ShopType ProviderType => ShopType.Answear;

        protected override int GetPrice(IList<IElement> elements)
        {
            var priceSpan = elements.First(e =>
                !string.IsNullOrEmpty(e.ClassName) &&
                e.ClassName.Contains("Price__currentPrice"));

            var fullPrice = priceSpan.InnerHtml;

            var spaceIndex = fullPrice.IndexOf(' ');
            var price = fullPrice.Substring(0, spaceIndex);
            
            return int.Parse(price);
        }

        protected override string GetTitle(IList<IElement> elements)
        {
            return elements
                .First(x =>
                    !string.IsNullOrEmpty(x.ClassName) &&
                    x.ClassName.Contains("productNameAndLogo"))
                .Children
                .First(x => x.TagName.ToLower() == "h1")
                .Text();
        }

        protected override Uri GetImageUrl(IHtmlDocument document)
        {
            const string selector = ".slick-current > div> .cardMedia >div > picture > img";
            var image = document.QuerySelector<IHtmlImageElement>(selector);
            var imageUrl = image.Source;

            return new Uri(imageUrl);
        }
    }
}