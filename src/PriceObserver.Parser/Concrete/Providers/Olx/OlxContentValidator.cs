using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using System;

namespace PriceObserver.Parser.Concrete.Providers.Olx;

public class OlxContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Olx;

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "meta[name='description']";

		var element = document.QuerySelector<IHtmlMetaElement>(selector);
		if (element is null || element.Content is null)
			return false;

		return element.Content.IndexOf("грн", StringComparison.Ordinal) > 0;
	}
}