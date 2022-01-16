﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Makeup;

public class MakeupContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Makeup;

    public bool IsAvailable(IHtmlDocument document)
    {
        const string selector = "link[href='https://schema.org/OutOfStock']";

        return document.QuerySelector<IHtmlLinkElement>(selector) is null;
    }

    public bool IsPriceExists(IHtmlDocument document)
    {
        const string selector = "span[itemprop=price]";
        
        return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
    }
}