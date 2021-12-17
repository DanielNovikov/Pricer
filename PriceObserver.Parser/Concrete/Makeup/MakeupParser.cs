using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Base;

namespace PriceObserver.Parser.Concrete.Makeup
{
    public class MakeupParser : ParserProviderBase
    {
        public override ShopKey ProviderType => ShopKey.Makeup;
        
        protected override int GetPrice(IHtmlDocument document)
        {
            const string selector = "span[itemprop=price]";
            var price = document.QuerySelector<IHtmlSpanElement>(selector)!.TextContent;

            return int.Parse(price);
        }

        protected override string GetTitle(IHtmlDocument document)
        {
            const string selector = "meta[name=KeyWords]";
            return document.QuerySelector<IHtmlMetaElement>(selector)!.Content;
        }

        protected override Uri GetImageUrl(IHtmlDocument document)
        {
            const string selector = "img[itemprop=image]";
            var source = document.QuerySelector<IHtmlImageElement>(selector)!.Source;
            
            return new Uri(source!);
        }
    }
}