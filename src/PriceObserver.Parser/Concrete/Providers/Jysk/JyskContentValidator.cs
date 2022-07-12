using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Jysk;

public class JyskContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Jysk;

	public bool IsAvailable(IHtmlDocument document)
	{
		return true;
	}

	public bool HasItemInfo(IHtmlDocument document)
	{	
		const string selector = "span[class=ssr-product-price__value]";

		return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
	}
}