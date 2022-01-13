using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums.Cache;
using PriceObserver.Data.InMemory.Seed.Resources.Seeders;

namespace PriceObserver.Data.InMemory.Seed.Resources;

public class ResourcesSeeder
{
    public static void Seed(IMemoryCache cache)
    {
        var resources = new List<Resource>();
            
        DialogSeeder.Seed(resources);
        UserActionSeeder.Seed(resources);
        ParserSeeder.Seed(resources);
        BackgroundSeeder.Seed(resources);
        MenuSeeder.Seed(resources);
        CommandsSeeder.Seed(resources);

        cache.Set(CacheKey.Resources, resources);
    }
}