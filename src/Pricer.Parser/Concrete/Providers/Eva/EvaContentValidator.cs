using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Eva;

public class EvaContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Eva;

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "meta[itemprop='price']";

		return document.QuerySelector<IHtmlMetaElement>(selector) is not null;
	}
}