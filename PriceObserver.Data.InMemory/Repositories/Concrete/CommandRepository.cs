using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Models.Enums.Cache;
using PriceObserver.Data.InMemory.Repositories.Abstract;

namespace PriceObserver.Data.InMemory.Repositories.Concrete;

public class CommandRepository : ICommandRepository
{
    private readonly IMemoryCache _cache;

    public CommandRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Command GetByKey(CommandKey key)
    {
        return _cache
            .Get<List<Command>>(CacheKey.Commands)
            .Single(x => x.Key == key);
    }

    public Command GetByResourceKey(ResourceKey resourceKey)
    {
        return _cache
            .Get<List<Command>>(CacheKey.Commands)
            .SingleOrDefault(x => x.ResourceKey == resourceKey);
    }
}