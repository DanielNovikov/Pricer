using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

public class ApiSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.Add(new Resource(ResourceKey.Api_NoHistory, "Нет истории"));
        
        resources.Add(new Resource(ResourceKey.Api_GrewUpSign, "📈"));
        resources.Add(new Resource(ResourceKey.Api_WentDownSign, "📉"));
    }
}