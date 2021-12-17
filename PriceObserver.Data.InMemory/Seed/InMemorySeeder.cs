﻿using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Seed.Dialog;
using PriceObserver.Data.InMemory.Seed.Resources;
using PriceObserver.Data.InMemory.Seed.Shops;

namespace PriceObserver.Data.InMemory.Seed
{
    public class InMemorySeeder
    {
        public static void Seed(IMemoryCache cache) 
        {
            ResourcesSeeder.Seed(cache);
            DialogSeeder.Seed(cache);
            ShopsSeeder.Seed(cache);
        }
    }
}