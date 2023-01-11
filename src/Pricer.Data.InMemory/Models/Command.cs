using Pricer.Data.InMemory.Models.Abstract;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Models;

public record Command(CommandKey Key, ResourceKey ResourceKey, Menu MenuToRedirect)
    : IReadonlyEntity<CommandKey>;