using System;
using System.Linq;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;
using Pricer.Parser.Extensions;

namespace Pricer.Parser.Concrete.Providers.IHerb;

public class IHerbParser : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.IHerb;

    public int GetPrice(IHtmlDocument document) => document.GetMetaAsInt("og:price:amount");

    public string GetTitle(IHtmlDocument document) => document.GetMetaOgTitle();

    public Uri GetImageUrl(IHtmlDocument document) => document.GetLinkAsImage();

    public bool IsAvailable(IHtmlDocument document) => true;

    public CurrencyKey GetCurrency(IHtmlDocument document)
    {
        var currency = document.GetMeta("og:price:currency");
        return Enum
            .GetValues<CurrencyKey>()
            .FirstOrDefault(x => x.ToString() == currency);
    }
}