using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Resources.Seeders;

public class BackgroundSeeder
{
    public static void Seed(IList<Resource> resources)
    {
	    resources.AddResource(
            ResourceKey.Background_ItemDeleted,
            @"❗️Товар <a href='{0}'>{1}</a> більше недоступний
ℹ {2}",
            @"❗️Товар <a href='{0}'>{1}</a> больше недоступний
ℹ {2}");

        resources.AddResource(
            ResourceKey.Background_ItemPriceWentDown,
            @"❗ {0}
📉 Ціна знизилася до <b>{1}</b>{2} (на <b>{3}</b>{4})",
            @"❗ {0}
📉 Цена снизилась до <b>{1}</b>{2} (на <b>{3}</b>{4})");
	    
        resources.AddResource(
            ResourceKey.Background_ItemPriceGrewUp,
            @"❗ {0}
📈 Ціна зросла до <b>{1}</b>{2} (на <b>{3}</b>{4})",
            @"❗ {0}
📈 Цена выросла до <b>{1}</b>{2} (на <b>{3}</b>{4})");
	    
	    resources.AddResource(
            ResourceKey.Background_ItemIsInStock,
            "❗️Товар <a href='{0}'>{1}</a> тепер є в наявності",
            "❗️Товар <a href='{0}'>{1}</a> теперь есть в наличии");
        
	    resources.AddResource(
		    ResourceKey.Background_ItemIsOutOfStock,
		    "❗️Товару <a href='{0}'>{1}</a> тепер немає в наявності",
		    "❗️Товара <a href='{0}'>{1}</a> теперь нет в наличии");

	    resources.AddResource(
            ResourceKey.Background_LogItemPriceChanged,
            @"❗️Ціна на товар змінилася з {0} на {1}
Посилання: {2}",
            @"❗️Цена на товар изменилась с {0} на {1}
Ссылка: {2}");
	    
	    resources.AddResource(ResourceKey.Background_DeleteItem, "Видалити 🗑", "Удалить 🗑");
	    resources.AddResource(ResourceKey.Background_GoByItemUrl, "Перейти", "Перейти");
    }
}