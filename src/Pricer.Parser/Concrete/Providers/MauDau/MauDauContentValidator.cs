using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.MauDau;

public class MauDauContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.MauDau;

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "div.product-side-info span.price_final";

		return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
	}
}