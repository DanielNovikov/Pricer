using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

public class DecorationsSeeder
{
	public static void Seed(
		List<ShopCategory> shopCategories,
		List<Shop> shops,
		List<Currency> currencies)
	{
		var decorationsShops = new List<Shop>
		{
			new Shop(
				"Pandora",
				ShopKey.Pandora,
				"e-pandora.ua",
				"pandora.svg",
				true,
				currencies.First(x => x.Key == CurrencyKey.UAH))
		};

		var decorationsShopCategory = new ShopCategory(ResourceKey.ShopCategory_Decorations, "💍", decorationsShops);
        
		shopCategories.Add(decorationsShopCategory);
		shops.AddRange(decorationsShops);
	}
}