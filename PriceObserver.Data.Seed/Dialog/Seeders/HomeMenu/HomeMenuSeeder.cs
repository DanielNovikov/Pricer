using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers;

namespace PriceObserver.Data.Seed.Dialog.Seeders.HomeMenu
{
    public class HomeMenuSeeder
    {
        public static Menu Seed(ApplicationDbContext context)
        {
            return MenuInitializer.Initialize(
                context,
                MenuType.Home,
                "Выберите что хотите сделать ⬇",
                false,
                isDefault: true);
        }
    }
}