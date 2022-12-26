using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Olx;

public class OlxParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.Olx;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "meta[name='description']";

		var priceElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
			throw new ArgumentNullException($"{nameof(OlxParser)}:{nameof(GetPrice)}:Element");
		
		var priceElementContent = priceElement.Content ??
			throw new ArgumentNullException($"{nameof(OlxParser)}:{nameof(GetPrice)}:Element");
		
		var priceSeparatorIndex = priceElementContent.IndexOf(" ", StringComparison.Ordinal);
		var price = priceElementContent.Substring(0, priceSeparatorIndex);

		return int.Parse(price);
	}

	public string GetTitle(IHtmlDocument document)
	{
		const string selector = "h1[data-cy='ad_title']";
		
		var titleElement = document.QuerySelector<IHtmlHeadingElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(OlxParser)}:{nameof(GetTitle)}:Element");

		return titleElement.TextContent.Trim();
	}

	public Uri GetImageUrl(IHtmlDocument document)
	{
		const string selector = "meta[property='og:image']";
        
		var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(OlxParser)}:{nameof(GetImageUrl)}:Element");
        
		var imageSource = imageElement.Content ??
		    throw new ArgumentNullException($"{nameof(OlxParser)}:{nameof(GetImageUrl)}:Element:Content"); 
        
		return new Uri(imageSource);
	}

	public bool IsAvailable(IHtmlDocument document)
	{
		return true;
	}

	public CurrencyKey GetCurrency(IHtmlDocument document)
	{
		const string selector = "meta[name='description']";

		var priceElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
			throw new ArgumentNullException($"{nameof(OlxParser)}:{nameof(GetPrice)}:Element");
		
		var priceElementContent = priceElement.Content ??
		    throw new ArgumentNullException($"{nameof(OlxParser)}:{nameof(GetPrice)}:Element");

		return priceElementContent.Contains('$') ? CurrencyKey.USD : CurrencyKey.UAH;
	}
}