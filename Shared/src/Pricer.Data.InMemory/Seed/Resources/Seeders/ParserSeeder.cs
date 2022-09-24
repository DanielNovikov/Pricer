using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Resources.Seeders;

public class ParserSeeder
{
    public static void Seed(IList<Resource> resources)
    {
        resources.AddResource(
            ResourceKey.Parser_PageNotFound,
            "Сторінку не знайдено",
            "Страница не найдена");

        resources.AddResource(
            ResourceKey.Parser_NoItemInfoOnPage,
            "На сторінці немає інформації про товар",
            "На странице нет информации о товаре");
    }
}