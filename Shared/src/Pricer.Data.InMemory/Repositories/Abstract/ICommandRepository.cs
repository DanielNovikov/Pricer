using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Repositories.Abstract;

public interface ICommandRepository : IReadOnlyRepository<Command, CommandKey>
{
    Command GetByResourceKey(ResourceKey resourceKey);
}