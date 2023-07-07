using System;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Eva;

public class EvaParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.Eva;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "div.sf-price > span";
		
		var priceElement = document.QuerySelector<IHtmlSpanElement>(selector) ??
			throw new ArgumentNullException($"{nameof(EvaParser)}:{nameof(GetPrice)}:Element");

		var price = priceElement.TextContent;
		var formattedPrice = price
			.Substring(0, price.IndexOf("грн", StringComparison.Ordinal))
			.Replace(" ", string.Empty);

		return (int)double.Parse(formattedPrice);
	}

	public string GetTitle(IHtmlDocument document)
	{
		const string selector = "h1.sf-heading__title";
		
		var titleElement = document.QuerySelector<IHtmlHeadingElement>(selector) ??
			throw new ArgumentNullException($"{nameof(EvaParser)}:{nameof(GetTitle)}:Element");

		return titleElement.TextContent.Trim();
	}

	public Uri GetImageUrl(IHtmlDocument document)
	{
		const string selector = ".glide__slides .a-image-zoom img";
		
		var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(EvaParser)}:{nameof(GetImageUrl)}:Element");

		var imageSource = imageElement.Source ??
		    throw new ArgumentNullException($"{nameof(EvaParser)}:{nameof(GetImageUrl)}:Element:Content");

		return new Uri(imageSource);
	}

	public bool IsAvailable(IHtmlDocument document)
	{
		const string selector = "meta[content='OutOfStock']";

		return document.QuerySelector<IHtmlMetaElement>(selector) is null;
	}

	public CurrencyKey GetCurrency(IHtmlDocument document)
	{
		return CurrencyKey.UAH;
	}
}