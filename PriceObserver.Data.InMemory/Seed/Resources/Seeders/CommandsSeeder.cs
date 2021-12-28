using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

public class CommandsSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.Add(new Resource(ResourceKey.Command_Back, "Назад ◀"));
        resources.Add(new Resource(ResourceKey.Command_Help, "Помощь 🆘"));
        resources.Add(new Resource(ResourceKey.Command_Add, "Добавить ➕"));
        resources.Add(new Resource(ResourceKey.Command_AllItems, "Все товары ℹ"));
        resources.Add(new Resource(ResourceKey.Command_Shops, "Магазины 🛒"));
        resources.Add(new Resource(ResourceKey.Command_Website, "Сайт 🌍"));
        resources.Add(new Resource(ResourceKey.Command_WriteToSupport, "Поддержка 👨🏻‍💻"));
    }
}