using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Jysk;

public class JyskParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.Jysk;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "span[class=ssr-product-price__value]";

		var priceElement = document.QuerySelector<IHtmlSpanElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(JyskParser)}:{nameof(GetPrice)}:Element");

		var price = priceElement.TextContent
			.Replace(" ", string.Empty)
			.Replace("грн.", string.Empty);

		return int.Parse(price);
	}

	public string GetTitle(IHtmlDocument document)
	{
		const string selector = "main span[class=product-name]";

		var titleElement = document.QuerySelector<IHtmlSpanElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(JyskParser)}:{nameof(GetTitle)}:Element");

		return titleElement.TextContent;
	}

	public Uri GetImageUrl(IHtmlDocument document)
	{
		const string selector = "button[class*=btn-carousel-item-zoom] > img";
		
		var imageElement = document.QuerySelector<IHtmlImageElement>(selector) ?? 
		    throw new ArgumentNullException($"{nameof(JyskParser)}:{nameof(GetImageUrl)}:Element");

		var imageSource = imageElement.Source ?? 
		    throw new ArgumentNullException($"{nameof(JyskParser)}:{nameof(GetImageUrl)}:Element:Content");
        
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