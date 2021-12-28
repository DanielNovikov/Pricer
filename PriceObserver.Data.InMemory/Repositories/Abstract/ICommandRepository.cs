using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Repositories.Abstract;

public interface ICommandRepository
{
    Command GetByKey(CommandKey key);

    Command GetByResourceKey(ResourceKey resourceKey);
}