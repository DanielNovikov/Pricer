using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Prostor;

public class ProstorParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.Prostor;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "meta[itemprop=price]";
        
		var priceElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(ProstorParser)}:{nameof(GetPrice)}:Element");

		var price = priceElement.Content ?? 
		    throw new ArgumentNullException($"{nameof(ProstorParser)}:{nameof(GetPrice)}:ElementContent");
        
		return int.Parse(price);
	}

	public string GetTitle(IHtmlDocument document)
	{
		const string selector = "meta[property='og:title']";
        
		var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(ProstorParser)}:{nameof(GetTitle)}:Element");

		var titleContent = titleElement.Content ??
			throw new ArgumentNullException($"{nameof(ProstorParser)}:{nameof(GetTitle)}:Element:Content");		                   
		
		return titleContent.Replace(' ', ' ');
	}

	public Uri GetImageUrl(IHtmlDocument document)
	{
		const string selector = "meta[property='og:image']";
        
		var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
		    throw new ArgumentNullException($"{nameof(ProstorParser)}:{nameof(GetImageUrl)}:Element");
        
		var imageSource = imageElement.Content ?? 
		    throw new ArgumentNullException($"{nameof(ProstorParser)}:{nameof(GetImageUrl)}:Element:Content");

		var formattedImageSource = imageSource.Replace(".png", ".webp");
		
		return new Uri(formattedImageSource);
	}

	public bool IsAvailable(IHtmlDocument document)
	{
		const string selector = "div.product-header__availability--out-of-stock";

		return document.QuerySelector<IHtmlDivElement>(selector) is null;
	}

	public CurrencyKey GetCurrency(IHtmlDocument document)
	{
		return CurrencyKey.UAH;
	}
}