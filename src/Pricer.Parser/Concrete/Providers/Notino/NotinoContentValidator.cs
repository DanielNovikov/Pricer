using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Notino;

public class NotinoContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Notino;

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "div#pd-price > span:first-child";

		return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
	}
}