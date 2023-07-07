using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Eva;

public class EvaContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Eva;

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "div.sf-price > span";

		return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
	}
}