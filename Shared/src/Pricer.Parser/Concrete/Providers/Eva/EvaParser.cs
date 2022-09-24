using System;
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
		const string selector = "meta[itemprop='price']";
		
		var priceElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
			throw new ArgumentNullException($"{nameof(EvaParser)}:{nameof(GetPrice)}:Element");

		var price = priceElement.Content ?? 
		    throw new ArgumentNullException($"{nameof(EvaParser)}:{nameof(GetPrice)}:Element:Content");
        
		return (int)double.Parse(price);
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
		const string selector = "li.glide__slide:first-child > div noscript";
		
		var noScriptElement = document.QuerySelector<IHtmlElement>(selector) ??
			throw new ArgumentNullException($"{nameof(EvaParser)}:{nameof(GetImageUrl)}:Element");

		var noScriptContent = noScriptElement.InnerHtml;
		var	imageSource = noScriptContent.Split('"')[1];
		
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