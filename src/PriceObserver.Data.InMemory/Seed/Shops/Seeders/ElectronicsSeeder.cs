using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

public class ElectronicsSeeder
{	
	public static void Seed(List<ShopCategory> shopCategories, List<Shop> shops)
	{
		var electronicsShops = new List<Shop>
		{
			new Shop(
                "Rozetka",
                ShopKey.Rozetka,
                "rozetka.com.ua",
                "rozetka.png",
                false,
                new [] { "bt.rozetka.com.ua" }),
            
            new Shop(
                "Citrus",
                ShopKey.Citrus,
                "www.ctrs.com.ua",
                "citrus.png",
                false),
            
            new Shop(
                "Stylus",
                ShopKey.Stylus,
                "stylus.ua",
                "stylus.png",
                false),
            
            new Shop(
                "eStore",
                ShopKey.Estore,
                "estore.ua",
                "estore.png",
                true),
            
            new Shop(
                "MOYO",
                ShopKey.Moyo,
                "www.moyo.ua",
                "moyo.png",
                false),
            
            new Shop(
                "Comfy",
                ShopKey.Comfy,
                "comfy.ua",
                "comfy.png",
                true),
            
            new Shop(
                "Telemart",
                ShopKey.Telemart,
                "telemart.ua",
                "telemart.png",
                false),
            
            new Shop(
                "Алло",
                ShopKey.Allo,
                "allo.ua",
                "allo.png",
                false)
		};

		var electronicsShopCategory = new ShopCategory(ResourceKey.ShopCategory_Electronics, "📱", electronicsShops);
        
		shopCategories.Add(electronicsShopCategory);
		shops.AddRange(electronicsShops);
	}
}