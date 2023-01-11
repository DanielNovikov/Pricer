using System;
using System.Collections.Generic;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Parser.Abstract;

public interface IRequestHeadersBuilder
{
    IReadOnlyDictionary<string, string> Build(Uri url, ShopKey shopKey);
}