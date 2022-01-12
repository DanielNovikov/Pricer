using System;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete.Farfetch;

public class FarfetchParserProvider : IParserProvider
{
    public ShopKey ProviderKey => ShopKey.Farfetch;
        
    public int GetPrice(IHtmlDocument document)
    {
        const string discountPriceSelector = "strong[data-tstid=priceInfo-onsale]";
        const string fullPriceSelector = "span[data-tstid=priceInfo-original]";

        var priceElement = 
            document.QuerySelector<IHtmlElement>(discountPriceSelector) ??
            document.QuerySelector<IHtmlElement>(fullPriceSelector);

        var priceString = priceElement!.Text();
        var formattedPriceString = priceString
            .Substring(0, priceString.IndexOf(' '))
            .Replace(" ", string.Empty);

        const int currency = 30;

        return int.Parse(formattedPriceString) * currency;
    }

    public string GetTitle(IHtmlDocument document)
    {
        const string descriptionSelector = "div[data-tstid=productOffer] span[data-tstid=cardInfo-description]";
        var description = document.QuerySelector<IHtmlElement>(descriptionSelector)!.Text();
        var formattedDescription = string.Concat(char.ToUpper(description[0]).ToString(), description[1..]);
            
        const string brandNameSelector = "div[data-tstid=productOffer] a[data-trk=pp_infobrd] > span";
        var brandName = document.QuerySelector<IHtmlElement>(brandNameSelector)!.Text();

        return $"{formattedDescription} {brandName}";
    }

    public Uri GetImageUrl(IHtmlDocument document)
    {
        const string selector = "img[data-test=imagery-img0]";
        var imageSource = document.QuerySelector<IHtmlImageElement>(selector)!.Source;

        return new Uri(imageSource);
    }
}