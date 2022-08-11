using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Prostor;

public class ProstorContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Prostor;

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "meta[itemprop=price]";

		return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
	}
}