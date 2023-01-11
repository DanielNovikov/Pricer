using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Pricer.Data.InMemory.Models.Abstract;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.InMemory.Seed;

namespace Pricer.Data.InMemory.Repositories.Concrete;

public class ReadOnlyRepositoryBase<T> : IReadOnlyRepository<T> 
    where T : IReadonlyEntity
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

    public IList<T> GetAll()
    {
        return MemoryCache.Get<List<T>>(CacheKey);
    }
}

public class ReadOnlyRepositoryBase<T, TKey> : ReadOnlyRepositoryBase<T>, IReadOnlyRepository<T, TKey> 
    where T : IReadonlyEntity<TKey>
{
    protected ReadOnlyRepositoryBase(
        IMemoryCache memoryCache,
        CacheKey cacheKey) 
        : base(memoryCache, cacheKey)
    {
    }

    public T GetByKey(TKey key)
    {
        return GetAll().SingleOrDefault(x => x.Key.Equals(key));
    }
}