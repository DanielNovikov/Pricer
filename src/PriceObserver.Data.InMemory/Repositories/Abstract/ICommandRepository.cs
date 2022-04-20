using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Repositories.Abstract;

public interface ICommandRepository : IReadOnlyRepository<Command, CommandKey>
{
    Command GetByResourceKey(ResourceKey resourceKey);
}