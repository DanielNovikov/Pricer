using PriceObserver.Data.InMemory.Models.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Models;

public record ResourceValue(string Text, LanguageKey LanguageKey) : IReadonlyEntity;