using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

public class DifferentSeeder
{
	public static void Seed(
		List<ShopCategory> shopCategories,
		List<Shop> shops,
		List<Currency> currencies)
	{
		var differentShops = new List<Shop>
		{
			new Shop(
				"Pandora",
				ShopKey.Pandora,
				"e-pandora.ua",
				"pandora.svg",
				true,
				currencies.First(x => x.Key == CurrencyKey.UAH)),
			
			new Shop(
				"JYSK",
				ShopKey.Jysk,
				"jysk.ua",
				"jysk.jpg",
				false,
				currencies.First(x => x.Key == CurrencyKey.UAH)),
			
			new Shop(
				"Епіцентр",
				ShopKey.Epicentr,
				"epicentrk.ua",
				"epicentr.png",
				false,
				currencies.First(x => x.Key == CurrencyKey.UAH))
		};

		var differentShopCategory = new ShopCategory(ResourceKey.ShopCategory_Different, "🛒", differentShops);
        
		shopCategories.Add(differentShopCategory);
		shops.AddRange(differentShops);
	}
}