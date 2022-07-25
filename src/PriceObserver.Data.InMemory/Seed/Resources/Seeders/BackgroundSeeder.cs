using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

public class BackgroundSeeder
{
    public static void Seed(IList<Resource> resources)
    {

#if DEBUG

        resources.AddResource(
            ResourceKey.Background_ItemDeleted,
            @"❗️Товар {1} більше недоступний
Посилання: {0}
ℹ {2}",
            @"❗️Товар {1} больше недоступен
Ссылка: {0}
ℹ {2}");
        
        resources.AddResource(
            ResourceKey.Background_ItemPriceWentDown,
            @"❗ {0}
📉 Ціна на товар знизилася до <b>{2}</b>{3} (на <b>{4}</b>{5})
Посилання: {1}",
            @"❗ {0}
📉 Цена на товар снизилась до <b>{2}</b>{3} (на <b>{4}</b>{5})
Ссылка: {1}");
        
        resources.AddResource(
	        ResourceKey.Background_ItemPriceGrewUp,
	        @"❗ {0}
📈 Ціна на товар зросла до <b>{2}</b>{3} (на <b>{4}</b>{5})
Посилання: {1}",
	        @"❗ {0}
📈 Цена на товар выросла до <b>{2}</b>{3} (на <b>{4}</b>{5})
Ссылка: {1}");
	    
	    resources.AddResource(
            ResourceKey.Background_ItemIsInStock,
            @"❗️Товар {1} тепер є в наявності
Посилання: {0}",
            @"❗️Товар {1} теперь есть в наличии
Ссылка: {0}");
        
	    resources.AddResource(
		    ResourceKey.Background_ItemIsOutOfStock,
		    @"❗️Товару <a href='{0}'>{1}</a> тепер немає в наявності
Посилання: {0}",
		    @"❗️Товара <a href='{0}'>{1}</a> теперь нет в наличии
Ссылка: {0}");

#else

        resources.AddResource(
            ResourceKey.Background_ItemDeleted,
            @"❗️Товар <a href='{0}'>{1}</a> більше недоступний
ℹ {2}",
            @"❗️Товар <a href='{0}'>{1}</a> больше недоступний
ℹ {2}");

        resources.AddResource(
            ResourceKey.Background_ItemPriceWentDown,
            @"❗ {0}
📉 Ціна на <a href='{1}'>товар</a> знизилася до <b>{2}</b>{3} (на <b>{4}</b>{5})",
            @"❗ {0}
📉 Цена на <a href='{1}'>товар</a> снизилась до <b>{2}</b>{3} (на <b>{4}</b>{5})");
	    
        resources.AddResource(
            ResourceKey.Background_ItemPriceGrewUp,
            @"❗ {0}
📉 Ціна на <a href='{1}'>товар</a> зросла до <b>{2}</b>{3} (на <b>{4}</b>{5})",
            @"❗ {0}
📉 Цена на <a href='{1}'>товар</a> выросла до <b>{2}</b>{3} (на <b>{4}</b>{5})");
	    
	    resources.AddResource(
            ResourceKey.Background_ItemIsInStock,
            "❗️Товар <a href='{0}'>{1}</a> тепер є в наявності",
            "❗️Товар <a href='{0}'>{1}</a> теперь есть в наличии");
        
	    resources.AddResource(
		    ResourceKey.Background_ItemIsOutOfStock,
		    "❗️Товару <a href='{0}'>{1}</a> тепер немає в наявності",
		    "❗️Товара <a href='{0}'>{1}</a> теперь нет в наличии");
	    
#endif

        resources.AddResource(
            ResourceKey.Background_LogItemPriceChanged,
            @"❗️Ціна на товар змінилася з {0} на {1}
Посилання: {2}",
            @"❗️Цена на товар изменилась с {0} на {1}
Ссылка: {2}");
    }
}