using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Seed.Shops.Seeders;

namespace Pricer.Data.InMemory.Seed.Shops;

public class ShopsSeeder
{
    public static void Seed(IMemoryCache cache)
    {
        var shopCategories = new List<ShopCategory>();
        var shops = new List<Shop>();
        
        ClothesSeeder.Seed(shopCategories, shops);
        CosmeticsSeeder.Seed(shopCategories, shops);
        ElectronicsSeeder.Seed(shopCategories, shops);
        FoodSeeder.Seed(shopCategories, shops);
        MarketPlacesSeeder.Seed(shopCategories, shops);
        DifferentSeeder.Seed(shopCategories, shops);
                
        cache.Set(CacheKey.Shops, shops);
        cache.Set(CacheKey.ShopCategories, shopCategories);
    }
}