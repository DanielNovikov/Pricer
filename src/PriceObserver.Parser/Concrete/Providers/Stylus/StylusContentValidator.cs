﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Stylus;

public class StylusContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Stylus;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "div.product-info-block div.regular-price";

        return document.QuerySelector<IHtmlDivElement>(selector) is not null;
    }
}