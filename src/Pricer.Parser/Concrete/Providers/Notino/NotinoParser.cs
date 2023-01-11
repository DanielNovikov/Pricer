using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Notino;

public class NotinoParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.Notino;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "div#pd-price > span:first-child";
		
		var priceElement = document.QuerySelector<IHtmlSpanElement>(selector) ?? 
			throw new ArgumentNullException($"{nameof(NotinoParser)}:{nameof(GetPrice)}:Element:Content");

		var price = priceElement.TextContent.Replace(" ", string.Empty);

		return int.Parse(price);
	}

	public string GetTitle(IHtmlDocument document)
	{
		const string selector = "meta[name=keywords]";
        
		var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
		    throw new ArgumentNullException($"{nameof(NotinoParser)}:{nameof(GetTitle)}:Element");

		return titleElement.Content;
	}

	public Uri GetImageUrl(IHtmlDocument document)
	{
		const string selector = "div[data-testid='main-image-wrapper'] img";
		
		var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ??
			throw new ArgumentNullException($"{nameof(NotinoParser)}:{nameof(GetImageUrl)}:Element");

		var imageSource = imageElement.Source ??
			throw new ArgumentNullException($"{nameof(NotinoParser)}:{nameof(GetImageUrl)}:Element:Content");

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