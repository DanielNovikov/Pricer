using System;
using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Parser.Abstract;

public interface IRequestHeadersBuilder
{
    IReadOnlyDictionary<string, string> Build(Uri url, ShopKey shopKey);
}