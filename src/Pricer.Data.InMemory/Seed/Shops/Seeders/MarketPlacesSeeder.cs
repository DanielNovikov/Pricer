using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Shops.Seeders;

public class MarketPlacesSeeder
{
	public static void Seed(List<ShopCategory> shopCategories, List<Shop> shops)
	{
		var marketPlaces = new List<Shop>
		{
			new Shop(
				"Prom",
				ShopKey.Prom,
				"prom.ua",
				"#8106ea",
				"#ffffff"),
			
			new Shop(
				"Shafa",
				ShopKey.Shafa,
				"shafa.ua",
				"#414141",
				"#ffffff"),
			
			new Shop(
				"OLX",
				ShopKey.Olx,
				"www.olx.ua",
				"#23e5db",
				"#002f34")
		};

		var marketPlacesCategory = new ShopCategory(ResourceKey.ShopCategory_MarketPlaces, "🛍", marketPlaces);
        
		shopCategories.Add(marketPlacesCategory);
		shops.AddRange(marketPlaces);
	}
}