using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models.Abstract;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.InMemory.Seed;

namespace PriceObserver.Data.InMemory.Repositories.Concrete;

public class ReadOnlyRepositoryBase<T, TKey> 
    : IReadOnlyRepository<T, TKey> 
    where T : IReadonlyEntity<TKey>
{
    private readonly IMemoryCache _memoryCache;
    private readonly CacheKey _cacheKey;

    public ReadOnlyRepositoryBase(
        IMemoryCache memoryCache,
        CacheKey cacheKey)
    {
        _memoryCache = memoryCache;
        _cacheKey = cacheKey;
    }

    public T GetByKey(TKey key)
    {
        return GetAll().SingleOrDefault(x => x.Key.Equals(key));
    }

    public IList<T> GetAll()
    {
        return _memoryCache.Get<List<T>>(_cacheKey);
    }
}