﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.Prom;

public class PromParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.Prom;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "span[data-qaid='product_price']";

		var priceElement = document.QuerySelector<IHtmlSpanElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(PromParser)}:{nameof(GetPrice)}:Element");;

		var price = priceElement.Attributes["data-qaprice"] ??
		    throw new ArgumentNullException($"{nameof(PromParser)}:{nameof(GetPrice)}:Element:Content");

		return int.Parse(price.Value);
	}

	public string GetTitle(IHtmlDocument document)
	{
		const string selector = "meta[property='og:title']";
        
		var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(PromParser)}:{nameof(GetTitle)}:Element");

		return titleElement.Content;
	}

	public Uri GetImageUrl(IHtmlDocument document)
	{
		const string selector = "link[as='image']";
		
		var imageElement = document.QuerySelector<IHtmlLinkElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(PromParser)}:{nameof(GetImageUrl)}:Element");
		
		var imageSource = imageElement.Href ??
		    throw new ArgumentNullException($"{nameof(PromParser)}:{nameof(GetImageUrl)}:Element:Content");

		return new Uri(imageSource);
	}

	public bool IsAvailable(IHtmlDocument document)
	{
		return true;
	}
}