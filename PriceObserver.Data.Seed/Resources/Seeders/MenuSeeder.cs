using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Resources.Initializers;

namespace PriceObserver.Data.Seed.Resources.Seeders
{
    public class MenuSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            ResourceInitializer.Initialize(context, ResourceKey.Menu_Home, "Выберите что хотите сделать ⬇");
            
            ResourceInitializer.Initialize(context, ResourceKey.Menu_NewItem, "Поделитесь ссылкой на желаемый товар 🆕");
            
            ResourceInitializer.Initialize(context, ResourceKey.Menu_Support, "Опишите с чем вы хотели-бы обратиться 📝");
        }
    }
}