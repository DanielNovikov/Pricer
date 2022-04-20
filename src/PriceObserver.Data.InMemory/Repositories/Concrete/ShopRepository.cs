using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.InMemory.Seed;

namespace PriceObserver.Data.InMemory.Repositories.Concrete;

public class ShopRepository : ReadOnlyRepositoryBase<Shop, ShopKey>, IShopRepository
{
    public ShopRepository(IMemoryCache cache) : base(cache, CacheKey.Shops)
    { }

    public Shop GetByHost(string host)
    {
        return GetAll().SingleOrDefault(x => 
            x.Host == host || 
            (x.SubHosts != null && x.SubHosts.Any(y => y == host)));
    }
}