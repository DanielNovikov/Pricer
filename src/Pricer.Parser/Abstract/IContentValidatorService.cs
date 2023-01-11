using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Parser.Models;

namespace Pricer.Parser.Abstract;

public interface IContentValidatorService
{
    ContentValidatorResult Validate(ShopKey providerKey, IHtmlDocument document);
}