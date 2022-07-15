using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

public class ElectronicsSeeder
{	
	public static void Seed(
		List<ShopCategory> shopCategories,
		List<Shop> shops,
		List<Currency> currencies)
	{
		var electronicsShops = new List<Shop>
		{
			new Shop(
                "Rozetka",
                ShopKey.Rozetka,
                "rozetka.com.ua",
                "rozetka.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH),
                new [] { "bt.rozetka.com.ua" }),
            
            new Shop(
                "Citrus",
                ShopKey.Citrus,
                "www.ctrs.com.ua",
                "citrus.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "Stylus",
                ShopKey.Stylus,
                "stylus.ua",
                "stylus.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "eStore",
                ShopKey.Estore,
                "estore.ua",
                "estore.png",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "MOYO",
                ShopKey.Moyo,
                "www.moyo.ua",
                "moyo.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "Comfy",
                ShopKey.Comfy,
                "comfy.ua",
                "comfy.png",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "Telemart",
                ShopKey.Telemart,
                "telemart.ua",
                "telemart.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "Алло",
                ShopKey.Allo,
                "allo.ua",
                "allo.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH))
		};

		var electronicsShopCategory = new ShopCategory(ResourceKey.ShopCategory_Electronics, "📱", electronicsShops);
        
		shopCategories.Add(electronicsShopCategory);
		shops.AddRange(electronicsShops);
	}
}