using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Resources.Seeders;

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