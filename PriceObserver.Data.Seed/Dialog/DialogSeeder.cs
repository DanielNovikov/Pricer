using PriceObserver.Data.Seed.Dialog.Initializers.HomeMenu;
using PriceObserver.Data.Seed.Dialog.Initializers.HomeMenu.Commands;
using PriceObserver.Data.Seed.Dialog.Initializers.NewItemMenu;
using PriceObserver.Data.Seed.Dialog.Initializers.NewItemMenu.Commands;

namespace PriceObserver.Data.Seed.Dialog
{
    public class DialogSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            var homeMenu = HomeMenuInitializer.Initialize(context);
            var newItemMenu = NewItemMenuInitializer.Initialize(context);

            AddCommandInitializer.Initialize(context, homeMenu, newItemMenu);
            AllItemsCommandInitializer.Initialize(context, homeMenu);
            ShopsCommandInitializer.Initialize(context, homeMenu);
            WebsiteCommandInitializer.Initialize(context, homeMenu);

            BackToHomeCommandInitializer.Initialize(context, newItemMenu, homeMenu);
        }
    }
}