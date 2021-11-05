using PriceObserver.Data.Seed.Dialog.Initializers.Common;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Seed.Dialog.Initializers.NewItemMenu
{
    public class NewItemMenuInitializer
    {
        public static Menu Initialize(ApplicationDbContext context, Menu parent)
        {
            return MenuInitializer.Initialize(
                context,
                MenuType.NewItem,
                "Вставьте ссылку на желаемый товар 🆕",
                true,
                parent: parent);
        }
    }
}