using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

public class BackgroundSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.Add(new Resource(
            ResourceKey.Background_ItemDeleted,
            "❗️Товар <a href='{0}'>{1}</a> больше недоступен\r\nℹ {2}"));
            
        resources.Add(new Resource(
            ResourceKey.Background_ItemPriceWentDown,
            @"❗ {0}
📉 Цена на <a href='{1}'>товар</a> снизилась до <b>{2}</b>{3} (на <b>{4}</b>{5})"));

        resources.Add(new Resource(
            ResourceKey.Background_LogItemPriceChanged,
            @"❗️Цена на товар изменилась с {0} на {1}
Ссылка: {2}"));
    }
}