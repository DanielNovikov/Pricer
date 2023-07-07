using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Shops.Seeders;

public class CosmeticsSeeder
{
	public static void Seed(List<ShopCategory> shopCategories, List<Shop> shops)
	{
		var cosmeticsShops = new List<Shop>
        {
            new Shop(
                "MAKEUP",
                ShopKey.Makeup,
                "makeup.com.ua",
                "#ffffff",
                "#000000"),
            
            new Shop(
                "Watsons",
                ShopKey.Watsons,
                "watsons.ua",
                "#00a0a0",
                "#fffffe"),
            
            new Shop(
                "EVA",
                ShopKey.Eva,
                "eva.ua",
                "#73bf43",
                "#ffffff"),
            
            new Shop(
                "Prostor",
                ShopKey.Prostor,
                "prostor.ua",
                "#d91d5c",
                "#ffffff"),
            
            new Shop(
                "Notino",
                ShopKey.Notino,
                "notino.ua",
                "#000000",
                "#ffffff"),

            new Shop(
                "iHerb",
                ShopKey.IHerb,
                "ua.iherb.com",
                "#468500",
                "#fffffc")
        };

        var cosmeticsShopCategory = new ShopCategory(ResourceKey.ShopCategory_Cosmetics, "💄", cosmeticsShops);
        
        shopCategories.Add(cosmeticsShopCategory);
        shops.AddRange(cosmeticsShops);
    }
}