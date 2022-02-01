using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

public class MenuSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.Add(new Resource(ResourceKey.Menu_Home, "Выберите что хотите сделать ⬇"));
            
        resources.Add(new Resource(ResourceKey.Menu_Support, "Опишите с чем вы хотели-бы обратиться 📝"));
    }
}