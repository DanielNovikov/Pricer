using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Resources.Initializers;

namespace PriceObserver.Data.Seed.Resources.Seeders
{
    public class CommandsSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            ResourceInitializer.Initialize(context, ResourceKey.Command_Back, "Назад ◀");
            ResourceInitializer.Initialize(context, ResourceKey.Command_Help, "Помощь 🆘");
            ResourceInitializer.Initialize(context, ResourceKey.Command_Add, "Добавить ➕");
            ResourceInitializer.Initialize(context, ResourceKey.Command_AllItems, "Все товары ℹ");
            ResourceInitializer.Initialize(context, ResourceKey.Command_Shops, "Магазины 🛒");
            ResourceInitializer.Initialize(context, ResourceKey.Command_Website, "Сайт 🌍");
            ResourceInitializer.Initialize(context, ResourceKey.Command_WriteToSupport, "Поддержка 👨🏻‍💻");
        }
    }
}