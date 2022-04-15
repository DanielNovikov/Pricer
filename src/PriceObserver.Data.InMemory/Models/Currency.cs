using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Models;

public record Currency(CurrencyKey Key, ResourceKey Title, ResourceKey Sign);