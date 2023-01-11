using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.InMemory.Seed;

namespace Pricer.Data.InMemory.Repositories.Concrete;

public class CommandRepository : ReadOnlyRepositoryBase<Command, CommandKey>, ICommandRepository
{
    public CommandRepository(IMemoryCache cache) : base(cache, CacheKey.Commands)
    { } 

    public Command GetByResourceKey(ResourceKey resourceKey)
    {
        return GetAll().SingleOrDefault(x => x.ResourceKey == resourceKey);
    }
}