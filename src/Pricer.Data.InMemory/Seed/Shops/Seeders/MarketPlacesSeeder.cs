using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

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
				"prom.png",
				false),
			
			new Shop(
				"Shafa",
				ShopKey.Shafa,
				"shafa.ua",
				"shafa.png",
				false),
			
			new Shop(
				"OLX",
				ShopKey.Olx,
				"www.olx.ua",
				"olx.png",
				false)
		};

		var marketPlacesCategory = new ShopCategory(ResourceKey.ShopCategory_MarketPlaces, "🛍", marketPlaces);
        
		shopCategories.Add(marketPlacesCategory);
		shops.AddRange(marketPlaces);
	}
}