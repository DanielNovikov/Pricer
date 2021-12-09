using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Resources.Initializers;

namespace PriceObserver.Data.Seed.Resources.Seeders
{
    public class ParserSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Parser_PageNotFound,
                "Страница не найдена");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Parser_ShopIsNotAvailable,
                "Магазин недоступен ❌");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Parser_NoPriceOnPage,
                "На странице нет цены");
        }
    }
}