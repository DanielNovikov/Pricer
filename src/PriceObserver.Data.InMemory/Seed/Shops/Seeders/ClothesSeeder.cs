using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

public class ClothesSeeder
{
	public static void Seed(List<ShopCategory> shopCategories, List<Shop> shops)
	{
		var clothesShops = new List<Shop>
        {
            new Shop(
                "Intertop",
                ShopKey.Intertop,
                "intertop.ua",
                "intertop.png",
                true),

            new Shop(
                "MdFashion",
                ShopKey.MdFashion,
                "md-fashion.com.ua",
                "md-fashion.jfif",
                true),

            new Shop(
                "Answear",
                ShopKey.Answear,
                "answear.ua",
                "answear.png",
                true),

            new Shop(
                "FARFETCH",
                ShopKey.Farfetch,
                "www.farfetch.com",
                "farfetch.png",
                true),

            new Shop(
                "Adidas",
                ShopKey.Adidas,
                "www.adidas.ua",
                "adidas.png",
                true),

            new Shop(
                "Modivo",
                ShopKey.Modivo,
                "modivo.ua",
                "modivo.png",
                false),
            
            new Shop(
                "Athletics",
                ShopKey.Athletics,
                "athletics.kiev.ua",
                "athletics.kiev.ua.png",
                false)
        };

        var clothesShopCategory = new ShopCategory(ResourceKey.ShopCategory_Clothes, "🥻", clothesShops);
        
        shopCategories.Add(clothesShopCategory);
        shops.AddRange(clothesShops);
    }
}