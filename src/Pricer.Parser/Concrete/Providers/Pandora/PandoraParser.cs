using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Pandora;

public class PandoraParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.Pandora;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "script[type='application/ld+json']";
		var scriptElement = document.QuerySelector<IHtmlScriptElement>(selector) ??
			throw new ArgumentNullException($"{nameof(PandoraParser)}:{nameof(GetPrice)}:Element");

		var scriptContent = scriptElement.TextContent;
		var pricePropertyStartIndex = scriptContent.IndexOf("\"price\":", StringComparison.Ordinal);
		var pricePropertyEndIndex = scriptContent.IndexOf(",", pricePropertyStartIndex, StringComparison.Ordinal);
        
		var priceString = scriptContent.Substring(
			pricePropertyStartIndex + 8,
			pricePropertyEndIndex - pricePropertyStartIndex - 8);

		return int.Parse(priceString);
	}

	public string GetTitle(IHtmlDocument document)
	{
		const string selector = "h1[itemprop='name']";
		
		var titleElement = document.QuerySelector<IHtmlHeadingElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(PandoraParser)}:{nameof(GetTitle)}:Element");
		
		return titleElement.TextContent.Trim();
	}

	public Uri GetImageUrl(IHtmlDocument document)
	{	
		const string selector = "meta[property='og:image']";

		var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
		    throw new ArgumentNullException($"{nameof(PandoraParser)}:{nameof(GetImageUrl)}:Element");

		var imageSource = imageElement.Content ??
		    throw new ArgumentNullException($"{nameof(PandoraParser)}:{nameof(GetImageUrl)}:Element:Content");
        
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