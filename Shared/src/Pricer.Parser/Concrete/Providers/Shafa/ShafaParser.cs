using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Shafa;

public class ShafaParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.Shafa;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "meta[property='og:price:amount']";
		
		var priceElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(ShafaParser)}:{nameof(GetPrice)}:Element");

		var price = priceElement.Content ??
			throw new ArgumentNullException($"{nameof(ShafaParser)}:{nameof(GetPrice)}:Element:Content");
        
		return int.Parse(price);
	}

	public string GetTitle(IHtmlDocument document)
	{
		const string selector = "h1.b-product__title";
		
		var titleElement = document.QuerySelector<IHtmlHeadingElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(ShafaParser)}:{nameof(GetTitle)}:Element");

		return titleElement.TextContent.Trim('\n', ' ', '.');
	}

	public Uri GetImageUrl(IHtmlDocument document)
	{
		const string selector = "li.b-product-gallery__additional-item:first-child img";
		
		var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(ShafaParser)}:{nameof(GetImageUrl)}:Element");
		
		var imageSource = imageElement.Source ??
			throw new ArgumentNullException($"{nameof(ShafaParser)}:{nameof(GetImageUrl)}:Element:Content");
		
		return new Uri(imageSource);
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