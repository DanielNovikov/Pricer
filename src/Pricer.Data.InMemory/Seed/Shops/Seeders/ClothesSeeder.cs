using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Shops.Seeders;

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
                "#009f9d",
                "#e7f7f6"),

            new Shop(
                "MdFashion",
                ShopKey.MdFashion,
                "md-fashion.com.ua",
                "#000000",
                "#ffffff"),

            new Shop(
                "Answear",
                ShopKey.Answear,
                "answear.ua",
                "#000000",
                "#ffffff"),

            new Shop(
                "FARFETCH",
                ShopKey.Farfetch,
                "www.farfetch.com",
                "#ffffff",
                "#000000"),

            new Shop(
                "Adidas",
                ShopKey.Adidas,
                "www.adidas.ua",
                "#000000",
                "#ffffff"),

            new Shop(
                "Modivo",
                ShopKey.Modivo,
                "modivo.ua",
                "#ffffff",
                "#000000"),
            
            new Shop(
                "Athletics",
                ShopKey.Athletics,
                "athletics.kiev.ua",
                "#545454",
                "#ffffff")
        };

        var clothesShopCategory = new ShopCategory(ResourceKey.ShopCategory_Clothes, "🥻", clothesShops);
        
        shopCategories.Add(clothesShopCategory);
        shops.AddRange(clothesShops);
    }
}