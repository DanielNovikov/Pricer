using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

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
                "megamarket.webp",
                true),
            
            new Shop(
                "ЕКО Маркет", 
                ShopKey.EkoMarket,
                "eko.zakaz.ua",
                "ecomarket.jpg",
                true),
            
            new Shop(
                "Varus", 
                ShopKey.Varus,
                "varus.zakaz.ua",
                "varus.png",
                true),
            
            new Shop(
                "Ашан", 
                ShopKey.Auchan,
                "auchan.zakaz.ua",
                "auchan.png",
                true),
            
            new Shop(
                "Столичний Ринок", 
                ShopKey.StolychnyiRynok,
                "stolychnyi.zakaz.ua",
                "stolychnyi.png",
                true),
            
            new Shop(
                "Новус", 
                ShopKey.Novus,
                "novus.zakaz.ua",
                "novus.jpg",
                true),
            
            new Shop(
                "UltraMarket", 
                ShopKey.UltraMarket,
                "ultramarket.zakaz.ua",
                "ultramarket.png",
                true),
            
            new Shop(
                "MauDau", 
                ShopKey.MauDau,
                "maudau.com.ua",
                "maudau.png",
                false)
		};

        // new Shop(
        //     "JYSK",
        //     ShopKey.Jysk,
        //     "jysk.ua",
        //     "jysk.jpg",
        //     false,
        //     currencies.First(x => x.Key == CurrencyKey.UAH)),
        
		var foodShopCategory = new ShopCategory(ResourceKey.ShopCategory_Food, "🍽", foodShops);
        
		shopCategories.Add(foodShopCategory);
		shops.AddRange(foodShops);
	}
}