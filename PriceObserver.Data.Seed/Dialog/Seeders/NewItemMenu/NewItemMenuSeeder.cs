using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers;

namespace PriceObserver.Data.Seed.Dialog.Seeders.NewItemMenu
{
    public class NewItemMenuSeeder
    {
        public static Menu Seed(ApplicationDbContext context, Menu parent)
        {
            return MenuInitializer.Initialize(
                context,
                MenuType.NewItem,
                ResourceKey.Menu_NewItem,
                true,
                parent: parent);
        }
    }
}