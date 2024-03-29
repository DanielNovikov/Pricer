﻿using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Resources.Seeders;

public class CurrencySeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.AddResource(ResourceKey.Currency_UAH_Title, " грн.");
        resources.AddResource(ResourceKey.Currency_EUR_Title, "€");
        resources.AddResource(ResourceKey.Currency_USD_Title, "$");
        
        resources.AddResource(ResourceKey.Currency_UAH_Sign, "₴");
        resources.AddResource(ResourceKey.Currency_EUR_Sign, "€");
        resources.AddResource(ResourceKey.Currency_USD_Sign, "$");
    }
}