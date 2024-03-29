﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Allo;

public class AlloContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Allo;

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string priceElementSelector = "meta[itemprop=price]";
		const string outOfStockButtonSelector = "button[class*=buy-button--out-stock]";
		
		return 
			document.QuerySelector<IHtmlMetaElement>(priceElementSelector) is not null ||
			document.QuerySelector<IHtmlButtonElement>(outOfStockButtonSelector) is not null;
	}
}