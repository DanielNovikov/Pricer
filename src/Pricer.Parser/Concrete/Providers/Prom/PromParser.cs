using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Prom;

public class PromParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.Prom;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "div[data-qaid='product_price']";

		var priceElement = document.QuerySelector<IHtmlDivElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(PromParser)}:{nameof(GetPrice)}:Element");;

		var priceAttribute = priceElement.Attributes["data-qaprice"] ??
		    throw new ArgumentNullException($"{nameof(PromParser)}:{nameof(GetPrice)}:Element:Content");
		
		if (!double.TryParse(priceAttribute.Value, out var price))
			throw new ArgumentException($"{nameof(PromParser)}:{nameof(GetPrice)}:Element:Content:Value");

		return (int)price;
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
		
		var imageSourceAttribute = imageElement.Href ??
		    throw new ArgumentNullException($"{nameof(PromParser)}:{nameof(GetImageUrl)}:Element:Content");

		if (!Uri.TryCreate(imageSourceAttribute, UriKind.Absolute, out var imageSource))
			throw new ArgumentException($"{nameof(PromParser)}:{nameof(GetImageUrl)}:Element:Content:Value");

		return imageSource;
	}

	public bool IsAvailable(IHtmlDocument document)
	{
		return true;
	}

	public CurrencyKey GetCurrency(IHtmlDocument document)
	{
		return CurrencyKey.UAH;
	}
}