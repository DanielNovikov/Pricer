using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Models;

public record Menu(MenuKey Key, ResourceKey ResourceKey, bool CanExpectInput, bool IsDefault, Menu Parent)
{
    public IList<Command> Commands { get; } = new List<Command>();
}