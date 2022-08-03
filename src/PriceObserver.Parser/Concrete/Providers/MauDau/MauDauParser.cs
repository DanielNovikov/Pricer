﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.MauDau;

public class MauDauParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.MauDau;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "div.product-side-info span.price_final";
		
		var priceElement = document.QuerySelector<IHtmlSpanElement>(selector) ?? 
		    throw new ArgumentNullException($"{nameof(MauDauParser)}:{nameof(GetPrice)}:Element");

		var priceElementContent = priceElement.TextContent;
		var signIndex = priceElementContent.IndexOf("₴", StringComparison.Ordinal);
		var price = priceElementContent.Substring(0, signIndex).Trim();

		return int.Parse(price);
	}

	public string GetTitle(IHtmlDocument document)
	{
		const string selector = "h1.product-title";
		
		var titleElement = document.QuerySelector<IHtmlHeadingElement>(selector) ?? 
		    throw new ArgumentNullException($"{nameof(MauDauParser)}:{nameof(GetTitle)}:Element");

		return titleElement.TextContent.Trim();
	}

	public Uri GetImageUrl(IHtmlDocument document)
	{
		const string selector = "div.main-gallery div.slider-item:first-child > img";
		
		var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ?? 
		    throw new ArgumentNullException($"{nameof(MauDauParser)}:{nameof(GetImageUrl)}:Element");
		
		var imageSource = imageElement.Attributes["data-cfsrc"] ??
		    throw new ArgumentNullException($"{nameof(MauDauParser)}:{nameof(GetImageUrl)}:Element:Content");
		
		return new Uri(imageSource.Value);
	}

	public bool IsAvailable(IHtmlDocument document)
	{
		const string selector = "div.out-of-stock";

		return document.QuerySelector<IHtmlDivElement>(selector) is null;
	}
}