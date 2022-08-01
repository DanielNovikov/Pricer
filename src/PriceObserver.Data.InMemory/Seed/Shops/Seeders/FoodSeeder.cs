using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

public class FoodSeeder
{	
	public static void Seed(
		List<ShopCategory> shopCategories,
		List<Shop> shops,
		List<Currency> currencies)
	{
		var foodShops = new List<Shop>
		{
            new Shop(
                "МегаМаркет", 
                ShopKey.MegaMarket,
                "megamarket.zakaz.ua",
                "megamarket.webp",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "ЕКО Маркет", 
                ShopKey.EkoMarket,
                "eko.zakaz.ua",
                "ecomarket.jpg",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "Varus", 
                ShopKey.Varus,
                "varus.zakaz.ua",
                "varus.png",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "Ашан", 
                ShopKey.Auchan,
                "auchan.zakaz.ua",
                "auchan.png",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "Столичний Ринок", 
                ShopKey.StolychnyiRynok,
                "stolychnyi.zakaz.ua",
                "stolychnyi.png",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "Новус", 
                ShopKey.Novus,
                "novus.zakaz.ua",
                "novus.jpg",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "UltraMarket", 
                ShopKey.UltraMarket,
                "ultramarket.zakaz.ua",
                "ultramarket.png",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "MauDau", 
                ShopKey.MauDau,
                "maudau.com.ua",
                "maudau.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH))
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