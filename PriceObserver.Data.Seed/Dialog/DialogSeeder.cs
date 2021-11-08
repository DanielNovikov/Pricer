using PriceObserver.Data.Seed.Dialog.Initializers.Common;
using PriceObserver.Data.Seed.Dialog.Initializers.HomeMenu;
using PriceObserver.Data.Seed.Dialog.Initializers.HomeMenu.Commands;
using PriceObserver.Data.Seed.Dialog.Initializers.NewItemMenu;
using PriceObserver.Data.Seed.Dialog.Initializers.SupportMenu;

namespace PriceObserver.Data.Seed.Dialog
{
    public class DialogSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            var homeMenu = HomeMenuInitializer.Initialize(context);
            var newItemMenu = NewItemMenuInitializer.Initialize(context, homeMenu);
            var supportMenu = SupportMenuInitializer.Initialize(context, homeMenu);
            
            AddCommandInitializer.Initialize(context, homeMenu, newItemMenu);
            AllItemsCommandInitializer.Initialize(context, homeMenu);
            ShopsCommandInitializer.Initialize(context, homeMenu);
            WebsiteCommandInitializer.Initialize(context, homeMenu);
            WriteToSupportCommand.Initialize(context, homeMenu, supportMenu);

            BackCommandInitializer.Initialize(context);
        }
    }
}