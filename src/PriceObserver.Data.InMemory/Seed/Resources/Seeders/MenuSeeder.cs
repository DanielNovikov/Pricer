using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

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
    }
}