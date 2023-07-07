using Pricer.Data.InMemory.Models.Abstract;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Models;

public record Currency(CurrencyKey Key, string Title, string Sign)
    : IReadonlyEntity<CurrencyKey>;