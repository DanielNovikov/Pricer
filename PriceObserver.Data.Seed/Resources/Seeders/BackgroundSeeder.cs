using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Resources.Initializers;

namespace PriceObserver.Data.Seed.Resources.Seeders
{
    public class BackgroundSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Background_ItemDeleted,
                "❗️Товар <a href='{0}'>{1}</a> удалён\r\nℹ {2}");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Background_ItemPriceWentDown,
                "📉 Цена на <a href='{0}'>товар</a> снизилась до <b>{1}</b>");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Background_ItemPriceGrewUp,
                "📈 Цена на <a href='{0}'>товар</a> повысилась до <b>{1}</b>");
            
            ResourceInitializer.Initialize(
                context,
                ResourceKey.Background_ItemPriceChanged,
                @"❗️{0}
{1}");
        }
    }
}