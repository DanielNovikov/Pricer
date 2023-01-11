using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.Concrete.Providers.Epicentr;

public class EpicentrContentValidator : IContentValidator
{
	public ShopKey ProviderKey => ShopKey.Epicentr;

	public bool HasItemInfo(IHtmlDocument document)
	{ 
		const string selector = "div#MAIN div.p-block__status";
		
		return document.QuerySelector<IHtmlDivElement>(selector) is not null;
	}
}