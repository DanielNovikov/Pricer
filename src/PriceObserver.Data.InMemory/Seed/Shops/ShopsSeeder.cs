using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Seed.Shops.Seeders;

namespace PriceObserver.Data.InMemory.Seed.Shops;

public class ShopsSeeder
{
    public static void Seed(IMemoryCache cache)
    {
        var currencies = CurrenciesSeeder.Seed();
        
        var shopCategories = new List<ShopCategory>();
        var shops = new List<Shop>();
        
        ClothesSeeder.Seed(shopCategories, shops, currencies);
        CosmeticsSeeder.Seed(shopCategories, shops, currencies);
        ElectronicsSeeder.Seed(shopCategories, shops, currencies);
        FoodSeeder.Seed(shopCategories, shops, currencies);
        FurnitureSeeder.Seed(shopCategories, shops, currencies);
        DecorationsSeeder.Seed(shopCategories, shops, currencies);
        MarketPlacesSeeder.Seed(shopCategories, shops, currencies);
                
        cache.Set(CacheKey.Shops, shops);
        cache.Set(CacheKey.ShopCategories, shopCategories);
    }
}