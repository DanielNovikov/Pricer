﻿using AngleSharp.Html.Dom;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Abstract;

public interface IContentValidatorService
{
    ContentValidatorResult Validate(ShopKey providerKey, IHtmlDocument document);
}