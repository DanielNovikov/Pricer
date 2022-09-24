using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.InMemory.Seed;

namespace PriceObserver.Data.InMemory.Repositories.Concrete;

public class MenuRepository : ReadOnlyRepositoryBase<Menu, MenuKey>, IMenuRepository
{
    public MenuRepository(IMemoryCache cache) : base(cache, CacheKey.Menus)
    { }

    public Menu GetDefault()
    {
        return GetAll().Single(x => x.IsDefault);
    }
}