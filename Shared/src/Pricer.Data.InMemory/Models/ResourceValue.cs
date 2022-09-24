using Pricer.Data.InMemory.Models.Abstract;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Models;

public record ResourceValue(string Text, LanguageKey LanguageKey) : IReadonlyEntity;