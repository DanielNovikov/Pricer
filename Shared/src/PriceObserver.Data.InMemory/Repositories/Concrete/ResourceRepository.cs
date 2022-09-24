using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.InMemory.Seed;

namespace PriceObserver.Data.InMemory.Repositories.Concrete;

public class ResourceRepository : ReadOnlyRepositoryBase<Resource, ResourceKey>, IResourceRepository
{
    public ResourceRepository(IMemoryCache cache) : base(cache, CacheKey.Resources)
    { }

    public Resource GetByValue(string value)
    {
        return GetAll().SingleOrDefault(x => x.Values.Any(y => y.Text == value));
    }
}