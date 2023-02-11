using Pricer.Data.InMemory.Models.Abstract;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Models;

public record Shop(
    string Name,
    ShopKey Key,
    string Host,
    string BackgroundColor,
    string FontColor,
    string[] SubHosts = null)
    : IReadonlyEntity<ShopKey>;