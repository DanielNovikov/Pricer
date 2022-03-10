﻿using PriceObserver.Common.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Parser.Models;

public class ContentValidatorResult : ServiceErrorResult<ContentValidatorResult, ResourceKey>
{
    public static ContentValidatorResult OutOfStock()
    {
        return Fail(ResourceKey.Parser_OutOfStock);
    }
    
    public static ContentValidatorResult NoItemInfo()
    {
        return Fail(ResourceKey.Parser_NoItemInfoOnPage);
    }
}