using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

public class ApiSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.AddResource(
            ResourceKey.Api_NoHistory,
            "Нема історії",
            "Нет истории");

        resources.AddResource(ResourceKey.Api_GrewUpSign, "📈");
        resources.AddResource(ResourceKey.Api_WentDownSign, "📉");
        resources.AddResource(ResourceKey.Api_UrlTemplate,"https://{0}");
    }
}