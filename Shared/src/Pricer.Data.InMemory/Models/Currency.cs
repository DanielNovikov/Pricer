using Pricer.Data.InMemory.Models.Abstract;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Models;

public record Currency(CurrencyKey Key, ResourceKey Title, ResourceKey Sign)
    : IReadonlyEntity<CurrencyKey>;