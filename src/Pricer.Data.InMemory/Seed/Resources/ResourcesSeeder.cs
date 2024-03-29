﻿using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Seed.Resources.Seeders;

namespace Pricer.Data.InMemory.Seed.Resources;

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
        CurrencySeeder.Seed(resources);
        ApiSeeder.Seed(resources);
        ShopCategoriesSeeder.Seed(resources);
        AppNotificationsSeeder.Seed(resources);
        
        cache.Set(CacheKey.Resources, resources);
    }
}