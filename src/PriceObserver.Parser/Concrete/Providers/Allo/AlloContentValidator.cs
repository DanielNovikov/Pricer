using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Allo;

public class AlloContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Allo;

	public bool IsAvailable(IHtmlDocument document)
	{
		const string selector = "button[class*=buy-button--out-stock]";

		return document.QuerySelector<IHtmlButtonElement>(selector) is null;
	}

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "meta[itemprop=price]";

		return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
	}
}