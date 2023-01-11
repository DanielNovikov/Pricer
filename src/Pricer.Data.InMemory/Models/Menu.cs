using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Models;

public record Menu(MenuKey Key, ResourceKey Title, bool CanExpectInput, bool IsDefault, Menu Parent) 
    : IReadonlyEntity<MenuKey>
{
    public IList<Command> Commands { get; } = new List<Command>();
}