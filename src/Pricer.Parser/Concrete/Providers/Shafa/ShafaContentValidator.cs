using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Shafa;

public class ShafaContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Shafa;

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "meta[property='og:price:amount']";

		return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
	}
}