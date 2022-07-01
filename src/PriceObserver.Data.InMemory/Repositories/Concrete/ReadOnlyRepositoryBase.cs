using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models.Abstract;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.InMemory.Seed;

namespace PriceObserver.Data.InMemory.Repositories.Concrete;

public class ReadOnlyRepositoryBase<T, TKey> : IReadOnlyRepository<T, TKey> 
    where T : IReadonlyEntity<TKey>
{
    protected readonly IMemoryCache MemoryCache;
    protected readonly CacheKey CacheKey;

    protected ReadOnlyRepositoryBase(
        IMemoryCache memoryCache,
        CacheKey cacheKey)
    {
        MemoryCache = memoryCache;
        CacheKey = cacheKey;
    }

    public T GetByKey(TKey key)
    {
        return GetAll().SingleOrDefault(x => x.Key.Equals(key));
    }

    public IList<T> GetAll()
    {
        return MemoryCache.Get<List<T>>(CacheKey);
    }
}