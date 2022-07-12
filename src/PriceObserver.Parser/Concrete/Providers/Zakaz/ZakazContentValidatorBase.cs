using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Providers.Zakaz;

public abstract class ZakazContentValidatorBase : IContentValidator
{
	public abstract ShopKey ProviderKey { get; }

	public virtual bool IsAvailable(IHtmlDocument document)
	{
		const string selector = "div[class*=BigProductCardTopInfo__addToCartButtons] span[data-testid=icon_basket]";

		return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
	}

	public virtual bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "span[class*=Price__value_title]";

		return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
	}
}