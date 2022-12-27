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
                "makeup.png",
                false),
            
            new Shop(
                "Watsons",
                ShopKey.Watsons,
                "www.watsons.ua",
                "watsons.png",
                false),
            
            new Shop(
                "EVA",
                ShopKey.Eva,
                "eva.ua",
                "eva.png",
                true),
            
            new Shop(
                "Prostor",
                ShopKey.Prostor,
                "prostor.ua",
                "prostor.png",
                false),
            
            new Shop(
                "Notino",
                ShopKey.Notino,
                "www.notino.ua",
                "notino.png",
                false),
            
            new Shop(
                "iHerb",
                ShopKey.Notino,
                "ua.iherb.com",
                "iHerb.png",
                false)
        };

        var cosmeticsShopCategory = new ShopCategory(ResourceKey.ShopCategory_Cosmetics, "💄", cosmeticsShops);
        
        shopCategories.Add(cosmeticsShopCategory);
        shops.AddRange(cosmeticsShops);
    }
}