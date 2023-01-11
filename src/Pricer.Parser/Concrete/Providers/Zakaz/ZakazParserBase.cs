using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Zakaz;

public abstract class ZakazParserBase : IParserProvider
{
	public abstract ShopKey ProviderKey { get; }
	
	public virtual int GetPrice(IHtmlDocument document)
	{
		const string selector = "span[class*=Price__value_title]";
		
		var priceElement = document.QuerySelector<IHtmlSpanElement>(selector) ?? 
		    throw new ArgumentNullException($"{GetType().Name}:{nameof(GetPrice)}:Element");

		var price = priceElement.TextContent;
		var formattedPrice = price.Substring(0, price.IndexOf('.'));

		return int.Parse(formattedPrice);
	}

	public virtual string GetTitle(IHtmlDocument document)
	{
		const string selector = "meta[property='og:title']";
        
		var titleElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
		    throw new ArgumentNullException($"{GetType().Name}:{nameof(GetTitle)}:Element");

		return titleElement.Content;
	}

	public virtual Uri GetImageUrl(IHtmlDocument document)
	{
		const string selector = "meta[property='og:image']";
        
		var imageElement = document.QuerySelector<IHtmlMetaElement>(selector) ??
		    throw new ArgumentNullException($"{GetType().Name}:{nameof(GetImageUrl)}:Element");
        
		var imageSource = imageElement.Content ??
		    throw new ArgumentNullException($"{GetType().Name}:{nameof(GetImageUrl)}:Element:Content"); 
        
		return new Uri(imageSource);
	}

	public virtual bool IsAvailable(IHtmlDocument document)
	{
		const string selector = "div[class*=BigProductCardTopInfo__addToCartButtons] span[data-testid=icon_basket]";

		return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
	}

	public virtual CurrencyKey GetCurrency(IHtmlDocument document)
	{
		return CurrencyKey.UAH;
	}
}