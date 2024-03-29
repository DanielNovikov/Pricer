﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Citrus;

public class CitrusContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Citrus;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string selector = "div.price";

        return document.QuerySelector<IHtmlDivElement>(selector) is not null;
    }
}