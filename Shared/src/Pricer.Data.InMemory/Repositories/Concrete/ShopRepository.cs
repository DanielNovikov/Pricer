using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.InMemory.Seed;

namespace Pricer.Data.InMemory.Repositories.Concrete;

public class ShopRepository : ReadOnlyRepositoryBase<Shop, ShopKey>, IShopRepository
{
    public ShopRepository(IMemoryCache cache) : base(cache, CacheKey.Shops)
    { }

    public IEnumerable<Shop> GetAll(int? limit)
    {
        var shops = GetAll();

        return limit.HasValue
            ? shops.Take(limit.Value)
            : shops;
    }

    public Shop GetByHost(string host)
    {
        return GetAll().SingleOrDefault(x => 
            x.Host == host || 
            (x.SubHosts != null && x.SubHosts.Any(y => y == host)));
    }
}