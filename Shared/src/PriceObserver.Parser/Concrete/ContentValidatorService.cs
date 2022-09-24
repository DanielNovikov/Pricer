using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Concrete;

public class ContentValidatorService : IContentValidatorService
{
    private readonly IEnumerable<IContentValidator> _contentValidators;

    public ContentValidatorService(IEnumerable<IContentValidator> contentValidators)
    {
        _contentValidators = contentValidators;
    }

    public ContentValidatorResult Validate(ShopKey providerKey, IHtmlDocument document)
    {
        var contentValidator = _contentValidators.Single(x => x.ProviderKey == providerKey);
        
        if (!contentValidator.HasItemInfo(document))
            return ContentValidatorResult.NoItemInfo();

        return ContentValidatorResult.Success();
    }
}