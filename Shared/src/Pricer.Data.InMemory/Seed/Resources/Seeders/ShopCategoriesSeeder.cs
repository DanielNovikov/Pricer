using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Resources.Seeders;

public class ShopCategoriesSeeder
{
	public static void Seed(IList<Resource> resources)
	{
		resources.AddResource(ResourceKey.ShopCategory_Clothes, "Одяг", "Одежда");
		resources.AddResource(ResourceKey.ShopCategory_Electronics, "Електроніка", "Электроника");
		resources.AddResource(ResourceKey.ShopCategory_Food, "Продукти", "Продукты");
		resources.AddResource(ResourceKey.ShopCategory_Cosmetics, "Косметика", "Косметика");
		resources.AddResource(ResourceKey.ShopCategory_MarketPlaces, "Маркетплейси", "Маркетплейсы");
		resources.AddResource(ResourceKey.ShopCategory_Different, "Різне", "Разное");
	}
}