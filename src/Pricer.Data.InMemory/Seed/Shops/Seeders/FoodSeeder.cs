using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Shops.Seeders;

public class FoodSeeder
{	
	public static void Seed(List<ShopCategory> shopCategories, List<Shop> shops)
	{
		var foodShops = new List<Shop>
		{
            new Shop(
                "МегаМаркет", 
                ShopKey.MegaMarket,
                "megamarket.zakaz.ua",
                "#ffef11",
                "#ffffff"),
            
            new Shop(
                "ЕКО Маркет", 
                ShopKey.EkoMarket,
                "eko.zakaz.ua",
                "#c20003",
                "#ffffff"),
            
            new Shop(
                "Varus", 
                ShopKey.Varus,
                "varus.zakaz.ua",
                "#ee7c00",
                "#ffffff"),
            
            new Shop(
                "Ашан", 
                ShopKey.Auchan,
                "auchan.zakaz.ua",
                "#e10013",
                "#ffffff"),
            
            new Shop(
                "Столичний Ринок", 
                ShopKey.StolychnyiRynok,
                "stolychnyi.zakaz.ua",
                "#c50b1b",
                "#ffffff"),
            
            new Shop(
                "Novus", 
                ShopKey.Novus,
                "novus.zakaz.ua",
                "#51af30",
                "#fcfdfc"),
            
            new Shop(
                "UltraMarket", 
                ShopKey.UltraMarket,
                "ultramarket.zakaz.ua",
                "#ffffff",
                "#faac01"),
            
            new Shop(
                "MauDau", 
                ShopKey.MauDau,
                "maudau.com.ua",
                "#371f5e",
                "#a4ffb8")
		};
        
		var foodShopCategory = new ShopCategory(ResourceKey.ShopCategory_Food, "🍽", foodShops);
        
		shopCategories.Add(foodShopCategory);
		shops.AddRange(foodShops);
	}
}