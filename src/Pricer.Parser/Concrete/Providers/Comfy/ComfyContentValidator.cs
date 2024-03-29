﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Comfy;

public class ComfyContentValidator : IContentValidator
{
    public ShopKey ProviderKey => ShopKey.Comfy;

    public bool HasItemInfo(IHtmlDocument document)
    {
        const string discountPriceSelector = "meta[name='product:sale_price:amount']";
        const string fullPriceSelector = "meta[name='product:price:amount']";

        return 
            document.QuerySelector<IHtmlMetaElement>(discountPriceSelector) is not null ||
            document.QuerySelector<IHtmlMetaElement>(fullPriceSelector) is not null;
    }
}