using PriceObserver.Data.InMemory.Models.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Models;

public record Shop(
    string Name,
    ShopKey Key,
    string Host,
    string Logo,
    bool SameFormatImages,
    string[] SubHosts = null)
    : IReadonlyEntity<ShopKey>;