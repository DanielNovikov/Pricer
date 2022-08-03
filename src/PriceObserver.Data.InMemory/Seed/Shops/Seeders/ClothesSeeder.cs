using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

public class ClothesSeeder
{
	public static void Seed(
        List<ShopCategory> shopCategories,
        List<Shop> shops,
        List<Currency> currencies)
	{
		var clothesShops = new List<Shop>
        {
            new Shop(
                "Intertop",
                ShopKey.Intertop,
                "intertop.ua",
                "intertop.png",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),

            new Shop(
                "MdFashion",
                ShopKey.MdFashion,
                "md-fashion.com.ua",
                "md-fashion.jfif",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),

            new Shop(
                "Answear",
                ShopKey.Answear,
                "answear.ua",
                "answear.png",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),

            new Shop(
                "FARFETCH",
                ShopKey.Farfetch,
                "www.farfetch.com",
                "farfetch.png",
                true,
                currencies.First(x => x.Key == CurrencyKey.EUR)),

            new Shop(
                "Adidas",
                ShopKey.Adidas,
                "www.adidas.ua",
                "adidas.png",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),

            new Shop(
                "Modivo",
                ShopKey.Modivo,
                "modivo.ua",
                "modivo.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "Athletics",
                ShopKey.Athletics,
                "athletics.kiev.ua",
                "athletics.kiev.ua.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "Prom",
                ShopKey.Prom,
                "prom.ua",
                "prom.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH))
        };

        var clothesShopCategory = new ShopCategory(ResourceKey.ShopCategory_Clothes, "🥻", clothesShops);
        
        shopCategories.Add(clothesShopCategory);
        shops.AddRange(clothesShops);
    }
}