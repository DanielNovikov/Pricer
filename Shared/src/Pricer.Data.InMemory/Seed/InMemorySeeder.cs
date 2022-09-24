using Microsoft.Extensions.Caching.Memory;
using Pricer.Data.InMemory.Seed.Currencies;
using Pricer.Data.InMemory.Seed.Dialog;
using Pricer.Data.InMemory.Seed.Resources;
using Pricer.Data.InMemory.Seed.Shops;

namespace Pricer.Data.InMemory.Seed;

public class InMemorySeeder
{
    public static void Seed(IMemoryCache cache) 
    {
        ResourcesSeeder.Seed(cache);
        DialogSeeder.Seed(cache);
        ShopsSeeder.Seed(cache);
        CurrenciesSeeder.Seed(cache);
    }
}