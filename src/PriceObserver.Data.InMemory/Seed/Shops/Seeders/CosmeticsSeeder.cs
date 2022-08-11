using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

public class CosmeticsSeeder
{
	public static void Seed(
        List<ShopCategory> shopCategories,
        List<Shop> shops,
        List<Currency> currencies)
	{
		var cosmeticsShops = new List<Shop>
        {
            new Shop(
                "MAKEUP",
                ShopKey.Makeup,
                "makeup.com.ua",
                "makeup.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "Watsons",
                ShopKey.Watsons,
                "www.watsons.ua",
                "watsons.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "EVA",
                ShopKey.Eva,
                "eva.ua",
                "eva.png",
                true,
                currencies.First(x => x.Key == CurrencyKey.UAH)),
            
            new Shop(
                "Prostor",
                ShopKey.Prostor,
                "prostor.ua",
                "prostor.png",
                false,
                currencies.First(x => x.Key == CurrencyKey.UAH))
        };

        var cosmeticsShopCategory = new ShopCategory(ResourceKey.ShopCategory_Cosmetics, "💄", cosmeticsShops);
        
        shopCategories.Add(cosmeticsShopCategory);
        shops.AddRange(cosmeticsShops);
    }
}