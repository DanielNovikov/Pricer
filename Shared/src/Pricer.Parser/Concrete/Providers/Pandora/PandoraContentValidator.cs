using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Pandora;

public class PandoraContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Pandora;

	public bool HasItemInfo(IHtmlDocument document)
	{
		const string selector = "h1[itemprop='name']";

		return document.QuerySelector<IHtmlHeadingElement>(selector) is not null;
	}
}