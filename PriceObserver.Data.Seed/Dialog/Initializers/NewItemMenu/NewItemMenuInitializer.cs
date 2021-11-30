using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers.Common;

namespace PriceObserver.Data.Seed.Dialog.Initializers.NewItemMenu
{
    public class NewItemMenuInitializer
    {
        public static Menu Initialize(ApplicationDbContext context, Menu parent)
        {
            return MenuInitializer.Initialize(
                context,
                MenuType.NewItem,
                "Поделитесь ссылкой на желаемый товар 🆕",
                true,
                parent: parent);
        }
    }
}