using PriceObserver.Data.Seed.Dialog.Initializers.Common;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

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