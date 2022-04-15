using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Models;

public record Shop(
    string Name,
    ShopKey Key,
    string Host,
    string Logo,
    bool SameFormatImages,
    Currency Currency,
    string[] SubHosts = null);