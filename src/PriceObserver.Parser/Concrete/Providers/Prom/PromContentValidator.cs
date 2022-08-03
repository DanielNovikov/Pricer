using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Prom;

public class PromContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Prom;

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "span[data-qaid='product_price']";

		return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
	}
}