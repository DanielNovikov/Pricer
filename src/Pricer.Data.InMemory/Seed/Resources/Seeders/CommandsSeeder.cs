using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Resources.Seeders;

public class CommandsSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.AddResource(ResourceKey.Command_Help, "Допомога 🆘", "Помощь 🆘");
        resources.AddResource(ResourceKey.Command_Add, "Додати ➕", "Добавить ➕");
        resources.AddResource(ResourceKey.Command_AllItems, "Мої товари ℹ", "Мои товары ℹ");
        resources.AddResource(ResourceKey.Command_Shops, "Магазини 🛒", "Магазины 🛒");
        resources.AddResource(ResourceKey.Command_Website, "Сайт 🌍", "Сайт 🌍");
        resources.AddResource(ResourceKey.Command_WriteToSupport, "Підтримка 👨🏻‍💻", "Поддержка 👨🏻‍💻");
        resources.AddResource(ResourceKey.Command_Settings, "Налаштування ⚙️", "Настройки ⚙️");
        
        resources.AddResource(ResourceKey.Command_SelectUkrainianLanguage, "Українська 🇺🇦", "Украинский 🇺🇦");
        resources.AddResource(ResourceKey.Command_SelectRussianLanguage, "Російська 🇷🇺", "Российский 🇷🇺");
        
        resources.AddResource(ResourceKey.Command_SelectLanguage, "Мова 🌐", "Язык 🌐");
        resources.AddResource(ResourceKey.Command_TogglePriceGrowingNotifications, "Зростання ціни 🔔", "Возрастание цены 🔔");
        resources.AddResource(ResourceKey.Command_ChangeMinimumDiscount, "Мінімальна знижка 💸", "Минимальная скидка 💸");
        
        resources.AddResource(ResourceKey.Command_Back, "Назад ◀", "Назад ◀");
    }
}