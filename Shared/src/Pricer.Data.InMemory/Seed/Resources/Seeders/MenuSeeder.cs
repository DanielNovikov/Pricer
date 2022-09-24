using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Resources.Seeders;

public class MenuSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.AddResource(
            ResourceKey.Menu_Home,
            "Виберіть що хочете зробити ⬇",
            "Выберите что хотите сделать ⬇");

        resources.AddResource(
            ResourceKey.Menu_Support, 
            "Опишіть з чим ви хотіли б звернутися 📝",
            "Опишите с чем Вы хотели-бы обратиться 📝");
        
        resources.AddResource(
            ResourceKey.Menu_SelectLanguage,
            "Виберіть мову ⬇",
            "Выберите язык ⬇");
        
        resources.AddResource(
            ResourceKey.Menu_Settings,
            "Оберіть що хочете налаштувати 🔧",
            "Выберите что хотите настроить 🔧");
        
        resources.AddResource(
            ResourceKey.Menu_TogglePriceGrowing,
            "Чи хочете ви увімкнути/вимкнути повідомлення коли ціна зростає? 💸",
            "Хотите ли вы включить/выключить уведомления когда цена возрастает? 💸");
    }
}