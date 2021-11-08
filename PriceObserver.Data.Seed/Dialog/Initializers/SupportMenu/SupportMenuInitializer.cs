using PriceObserver.Data.Seed.Dialog.Initializers.Common;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

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