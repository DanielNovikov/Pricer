﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.MdFashion;

public class MdFashionContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.MdFashion;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string priceElementSelector = "span[class~=price_current]";
        const string imageElementSelector = "div[id=product0] img";
        
        return
            document.QuerySelector<IHtmlSpanElement>(priceElementSelector) is not null ||
            document.QuerySelector<IHtmlImageElement>(imageElementSelector) is not null;
    }
}