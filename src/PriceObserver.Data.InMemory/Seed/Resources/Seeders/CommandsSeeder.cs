using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

public class CommandsSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.AddResource(ResourceKey.Command_Back, "Назад ◀", "Назад ◀");
        resources.AddResource(ResourceKey.Command_Help, "Допомога 🆘", "Помощь 🆘");
        resources.AddResource(ResourceKey.Command_Add, "Додати ➕", "Добавить ➕");
        resources.AddResource(ResourceKey.Command_AllItems, "Мої товари ℹ", "Мои товары ℹ");
        resources.AddResource(ResourceKey.Command_Shops, "Магазини 🛒", "Магазины 🛒");
        resources.AddResource(ResourceKey.Command_Website, "Сайт 🌍", "Сайт 🌍");
        resources.AddResource(ResourceKey.Command_WriteToSupport, "Підтримка 👨🏻‍💻", "Поддержка 👨🏻‍💻");
        
        resources.AddResource(ResourceKey.Command_SelectUkrainianLanguage, "Українська 🇺🇦", "Украинский 🇺🇦");
        resources.AddResource(ResourceKey.Command_SelectRussianLanguage, "Російська 🇷🇺", "Российский 🇷🇺");
    }
}