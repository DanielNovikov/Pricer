using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.InMemory.Seed;

namespace PriceObserver.Data.InMemory.Repositories.Concrete;

public class CommandRepository : ReadOnlyRepositoryBase<Command, CommandKey>, ICommandRepository
{
    public CommandRepository(IMemoryCache cache) : base(cache, CacheKey.Commands)
    { } 

    public Command GetByResourceKey(ResourceKey resourceKey)
    {
        return GetAll().SingleOrDefault(x => x.ResourceKey == resourceKey);
    }
}