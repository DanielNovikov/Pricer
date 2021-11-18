using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers.Common;

namespace PriceObserver.Data.Seed.Dialog.Initializers.SupportMenu
{
    public class SupportMenuInitializer
    {
        public static Menu Initialize(ApplicationDbContext context, Menu parent)
        {
            return MenuInitializer.Initialize(
                context,
                MenuType.Support,
                "Опишите с чем вы хотели-бы обратиться 📝",
                true,
                parent: parent);
        }
    }
}