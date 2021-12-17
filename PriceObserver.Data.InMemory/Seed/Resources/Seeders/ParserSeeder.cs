using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders
{
    public class ParserSeeder
    {
        public static void Seed(IList<Resource> resources)
        {
            resources.Add(new Resource(
                ResourceKey.Parser_PageNotFound,
                "Страница не найдена"));
            
            resources.Add(new Resource(
                ResourceKey.Parser_ShopIsNotAvailable,
                "Магазин недоступен ❌"));
            
            resources.Add(new Resource(
                ResourceKey.Parser_NoPriceOnPage,
                "На странице нет цены"));
        }
    }
}