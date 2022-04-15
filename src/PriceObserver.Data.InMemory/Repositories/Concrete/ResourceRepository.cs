using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.InMemory.Seed;

namespace PriceObserver.Data.InMemory.Repositories.Concrete;

public class ResourceRepository : IResourceRepository
{
    private readonly IMemoryCache _cache;

    public ResourceRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Resource GetByKey(ResourceKey key)
    {
        return _cache
            .Get<List<Resource>>(CacheKey.Resources)
            .Single(x => x.Key == key);
    }

    public Resource GetByValue(string value)
    {
        return _cache
            .Get<List<Resource>>(CacheKey.Resources)
            .SingleOrDefault(x => x.Value == value);
    }
}