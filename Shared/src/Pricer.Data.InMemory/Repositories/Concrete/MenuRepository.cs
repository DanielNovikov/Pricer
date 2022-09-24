using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.InMemory.Seed;

namespace Pricer.Data.InMemory.Repositories.Concrete;

public class MenuRepository : ReadOnlyRepositoryBase<Menu, MenuKey>, IMenuRepository
{
    public MenuRepository(IMemoryCache cache) : base(cache, CacheKey.Menus)
    { }

    public Menu GetDefault()
    {
        return GetAll().Single(x => x.IsDefault);
    }
}