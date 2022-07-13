using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

public class ShopCategoriesSeeder
{
	public static void Seed(IList<Resource> resources)
	{
		resources.AddResource(ResourceKey.ShopCategory_Clothes, "Одяг", "Одежда");
		resources.AddResource(ResourceKey.ShopCategory_Electronics, "Електроніка", "Электроника");
		resources.AddResource(ResourceKey.ShopCategory_Food, "Продукти", "Продукты");
		resources.AddResource(ResourceKey.ShopCategory_Cosmetics, "Косметика", "Косметика");
		resources.AddResource(ResourceKey.ShopCategory_Furniture, "Меблі", "Мебель");
	}
}