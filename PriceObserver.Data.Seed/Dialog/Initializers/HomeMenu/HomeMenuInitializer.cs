using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers.Common;

namespace PriceObserver.Data.Seed.Dialog.Initializers.HomeMenu
{
    public class HomeMenuInitializer
    {
        public static Menu Initialize(ApplicationDbContext context)
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