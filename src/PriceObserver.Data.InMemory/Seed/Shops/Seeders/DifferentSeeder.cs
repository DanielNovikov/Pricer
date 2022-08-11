using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

public class DifferentSeeder
{
	public static void Seed(List<ShopCategory> shopCategories, List<Shop> shops)
	{
		var differentShops = new List<Shop>
		{
			new Shop(
				"Pandora",
				ShopKey.Pandora,
				"e-pandora.ua",
				"pandora.svg",
				true),
			
			new Shop(
				"JYSK",
				ShopKey.Jysk,
				"jysk.ua",
				"jysk.jpg",
				false),
			
			new Shop(
				"Епіцентр",
				ShopKey.Epicentr,
				"epicentrk.ua",
				"epicentr.png",
				false)
		};

		var differentShopCategory = new ShopCategory(ResourceKey.ShopCategory_Different, "🛒", differentShops);
        
		shopCategories.Add(differentShopCategory);
		shops.AddRange(differentShops);
	}
}