using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

public class MarketPlacesSeeder
{
	public static void Seed(
		List<ShopCategory> shopCategories,
		List<Shop> shops,
		List<Currency> currencies)
	{
		var marketPlaces = new List<Shop>
		{
			new Shop(
				"Prom",
				ShopKey.Prom,
				"prom.ua",
				"prom.png",
				false,
				currencies.First(x => x.Key == CurrencyKey.UAH)),
			
			new Shop(
				"Shafa",
				ShopKey.Shafa,
				"shafa.ua",
				"shafa.png",
				false,
				currencies.First(x => x.Key == CurrencyKey.UAH))
		};

		var marketPlacesCategory = new ShopCategory(ResourceKey.ShopCategory_MarketPlaces, "🛍", marketPlaces);
        
		shopCategories.Add(marketPlacesCategory);
		shops.AddRange(marketPlaces);
	}
}