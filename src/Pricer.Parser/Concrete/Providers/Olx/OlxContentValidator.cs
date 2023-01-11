using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Olx;

public class OlxContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Olx;

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string headerSelector = "h1[data-cy='ad_title']";
		
		var headerElement = document.QuerySelector<IHtmlHeadingElement>(headerSelector);
		if (headerElement is null)
			return false;
		
		const string metaSelector = "meta[name='description']";
		
		var metaElement = document.QuerySelector<IHtmlMetaElement>(metaSelector);
		if (metaElement is null || metaElement.Content is null)
			return false;

		return 
			!metaElement.Content.StartsWith("Обмін", StringComparison.Ordinal) &&
		    !metaElement.Content.StartsWith("Обмен", StringComparison.Ordinal);
	}
}