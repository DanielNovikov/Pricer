using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Pandora;

public class PandoraContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Pandora;

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "div.product div.product__price span";

		return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
	}
}