using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Models.Enums.Cache;
using PriceObserver.Data.InMemory.Repositories.Abstract;

namespace PriceObserver.Data.InMemory.Repositories.Concrete;

public class MenuRepository : IMenuRepository
{
    private readonly IMemoryCache _cache;

    public MenuRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Menu GetDefault()
    {
        return _cache
            .Get<List<Menu>>(CacheKey.Menus)
            .Single(x => x.IsDefault);
    }

    public Menu GetByKey(MenuKey key)
    {
        return _cache
            .Get<List<Menu>>(CacheKey.Menus)
            .Single(x => x.Key == key);
    }
}