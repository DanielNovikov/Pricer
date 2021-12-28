using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;

namespace PriceObserver.Data.InMemory.Repositories.Concrete;

public class ShopRepository : IShopRepository
{
    private readonly IMemoryCache _cache;

    public ShopRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Shop GetByKey(ShopKey key)
    {
        return GetAll().Single(x => x.Key == key);
    }

    public Shop GetByHost(string host)
    {
        return GetAll().Single(x => x.Host == host);
    }

    public IList<Shop> GetAll()
    {
        return _cache.Get<List<Shop>>(nameof(Shop));
    }
}