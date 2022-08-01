using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.Pandora;

public class PandoraParser : IParserProvider
{
	public ShopKey ProviderKey => ShopKey.Pandora;

	public int GetPrice(IHtmlDocument document)
	{
		const string selector = "div.product div.product__price span";

		var priceElement = document.QuerySelector<IHtmlSpanElement>(selector) ??
		    throw new ArgumentNullException($"{nameof(PandoraParser)}:{nameof(GetPrice)}:Element");
		
		var price = priceElement.TextContent.Split("\n")[1].Trim();
		return int.Parse(price);
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
}