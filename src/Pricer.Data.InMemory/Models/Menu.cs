using System.Collections.Generic;
using Pricer.Data.InMemory.Models.Abstract;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Models;

public record Menu(MenuKey Key, ResourceKey Title, bool CanExpectInput, bool IsDefault, Menu Parent) 
    : IReadonlyEntity<MenuKey>
{
    public IList<Command> Commands { get; } = new List<Command>();
}