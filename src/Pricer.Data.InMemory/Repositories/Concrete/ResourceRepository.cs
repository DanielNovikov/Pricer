using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.InMemory.Seed;

namespace Pricer.Data.InMemory.Repositories.Concrete;

public class ResourceRepository : ReadOnlyRepositoryBase<Resource, ResourceKey>, IResourceRepository
{
    public ResourceRepository(IMemoryCache cache) : base(cache, CacheKey.Resources)
    { }

    public Resource GetByValue(string value)
    {
        return GetAll().SingleOrDefault(x => x.Values.Any(y => y.Text == value));
    }
}