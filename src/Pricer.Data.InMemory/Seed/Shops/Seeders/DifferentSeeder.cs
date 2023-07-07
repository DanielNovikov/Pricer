using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Shops.Seeders;

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
				"#ff9aaa",
				"#ffffff"),
			
			new Shop(
				"JYSK",
				ShopKey.Jysk,
				"jysk.ua",
				"#004495",
				"#ffffff"),
			
			new Shop(
				"Епіцентр",
				ShopKey.Epicentr,
				"epicentrk.ua",
				"#254185",
				"#ffffff"),
			
			new Shop(
				"AUTO.RIA",
				ShopKey.AutoRia,
				"auto.ria.com",
				"#db5c4c",
				"#ffffff"),
		};

		var differentShopCategory = new ShopCategory(ResourceKey.ShopCategory_Different, "🛒", differentShops);
        
		shopCategories.Add(differentShopCategory);
		shops.AddRange(differentShops);
	}
}