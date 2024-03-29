﻿using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;
using Pricer.Parser.Models;

namespace Pricer.Parser.Concrete;

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