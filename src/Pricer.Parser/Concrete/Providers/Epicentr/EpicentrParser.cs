﻿using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Epicentr;

public class EpicentrParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.Epicentr;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "div[class='p-price__main']";

		var priceElement = document.QuerySelector<IHtmlDivElement>(selector) ??
			throw new ArgumentNullException($"{nameof(EpicentrParser)}:{nameof(GetPrice)}:Element");

		var price = priceElement.TextContent.Replace(" ", string.Empty);

		return int.Parse(price);
	}

	public string GetTitle(IHtmlDocument document)
	{
		const string selector = "meta[property='og:title']";
        
		var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(EpicentrParser)}:{nameof(GetTitle)}:Element");

		return titleElement.Content;
	}

	public Uri GetImageUrl(IHtmlDocument document)
	{
		const string selector = "meta[property='og:image']";
        
		var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(EpicentrParser)}:{nameof(GetImageUrl)}:Element");
        
		var imageSource = imageElement.Content ??
		    throw new ArgumentNullException($"{nameof(EpicentrParser)}:{nameof(GetImageUrl)}:Element:Content"); 
        
		return new Uri(imageSource);
	}

	public bool IsAvailable(IHtmlDocument document)
	{
		const string selector = "div[class='p-price__main']";

		return document.QuerySelector<IHtmlDivElement>(selector) is not null;
	}

	public CurrencyKey GetCurrency(IHtmlDocument document)
	{
		return CurrencyKey.UAH;
	}
}