using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

public class CurrencySeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.Add(new Resource(ResourceKey.Currency_UAH_Title, " грн."));
        resources.Add(new Resource(ResourceKey.Currency_EUR_Title, "€"));
        
        resources.Add(new Resource(ResourceKey.Currency_UAH_Sign, "₴"));
        resources.Add(new Resource(ResourceKey.Currency_EUR_Sign, "€"));
    }
}