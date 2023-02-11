using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Shops.Seeders;

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
                "#00a03e",
                "#191c14",
                new [] { "bt.rozetka.com.ua", "hard.rozetka.com.ua" }),
            
            new Shop(
                "Citrus",
                ShopKey.Citrus,
                "www.ctrs.com.ua",
                "#f6731c",
                "#fefefe"),
            
            new Shop(
                "Stylus",
                ShopKey.Stylus,
                "stylus.ua",
                "#13b6d3",
                "#fffeff"),
            
            new Shop(
                "eStore",
                ShopKey.Estore,
                "estore.ua",
                "#000000",
                "#ffffff"),
            
            new Shop(
                "MOYO",
                ShopKey.Moyo,
                "www.moyo.ua",
                "#00acdc",
                "#ffffff"),
            
            new Shop(
                "Comfy",
                ShopKey.Comfy,
                "comfy.ua",
                "#43b02a",
                "#ffffff"),
            
            new Shop(
                "Telemart",
                ShopKey.Telemart,
                "telemart.ua",
                "#484f54",
                "#ffffff"),
            
            new Shop(
                "Алло",
                ShopKey.Allo,
                "allo.ua",
                "#ef363f",
                "#ffffff")
		};

		var electronicsShopCategory = new ShopCategory(ResourceKey.ShopCategory_Electronics, "📱", electronicsShops);
        
		shopCategories.Add(electronicsShopCategory);
		shops.AddRange(electronicsShops);
	}
}