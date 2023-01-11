using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Zakaz;

public abstract class ZakazContentValidatorBase : IContentValidator
{
	public abstract ShopKey ProviderKey { get; }

	public virtual bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "span[class*=Price__value_title]";

		return document.QuerySelector<IHtmlSpanElement>(selector) is not null;
	}
}