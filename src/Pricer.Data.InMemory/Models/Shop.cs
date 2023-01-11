using Pricer.Data.InMemory.Models.Abstract;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Models;

public record Shop(
    string Name,
    ShopKey Key,
    string Host,
    string Logo,
    bool SameFormatImages,
    string[] SubHosts = null)
    : IReadonlyEntity<ShopKey>;