using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

public class FurnitureSeeder
{
	public static void Seed(
		List<ShopCategory> shopCategories,
		List<Shop> shops,
		List<Currency> currencies)
	{
		var furnitureShops = new List<Shop>
		{
			new Shop(
				"JYSK",
				ShopKey.Jysk,
				"jysk.ua",
				"jysk.jpg",
				false,
				currencies.First(x => x.Key == CurrencyKey.UAH)),
		};

		var furnitureShopCategory = new ShopCategory(ResourceKey.ShopCategory_Furniture, "🏠", furnitureShops);
        
		shopCategories.Add(furnitureShopCategory);
		shops.AddRange(furnitureShops);
	}
}