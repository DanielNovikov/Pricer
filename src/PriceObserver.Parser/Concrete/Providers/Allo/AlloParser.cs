using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.Allo;

public class AlloParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.Allo;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "meta[itemprop=price]";

		var priceElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(AlloParser)}:{nameof(GetPrice)}:Element");

		var price = priceElement.Content ??
		    throw new ArgumentNullException($"{nameof(AlloParser)}:{nameof(GetPrice)}:Element:Content");
        
		return int.Parse(price);
	}

	public string GetTitle(IHtmlDocument document)
	{
		const string selector = "h1[class=p-view__header-title]";
        
		var titleElement = document.QuerySelector<IHtmlElement>(selector) ?? 
		    throw new ArgumentNullException($"{nameof(AlloParser)}:{nameof(GetTitle)}:Element");

		return titleElement.TextContent;
	}

	public Uri GetImageUrl(IHtmlDocument document)
	{
		const string selector = "meta[property='og:image']";

		var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ?? 
		    throw new ArgumentNullException($"{nameof(AlloParser)}:{nameof(GetImageUrl)}:Element");

		var imageSource = imageElement.Content ??
		    throw new ArgumentNullException($"{nameof(AlloParser)}:{nameof(GetImageUrl)}:Element:Content");
        
		return new Uri(imageSource);
	}

	public bool IsAvailable(IHtmlDocument document)
	{
		const string selector = "button[class*=buy-button--out-stock]";

		return document.QuerySelector<IHtmlButtonElement>(selector) is null;
	}
}