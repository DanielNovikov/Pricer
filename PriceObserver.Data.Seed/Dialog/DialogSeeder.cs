using PriceObserver.Data.Seed.Dialog.Initializers;
using PriceObserver.Data.Seed.Dialog.Seeders.HomeMenu;
using PriceObserver.Data.Seed.Dialog.Seeders.HomeMenu.Commands;
using PriceObserver.Data.Seed.Dialog.Seeders.NewItemMenu;
using PriceObserver.Data.Seed.Dialog.Seeders.SupportMenu;

namespace PriceObserver.Data.Seed.Dialog
{
    public class DialogSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            var homeMenu = HomeMenuSeeder.Seed(context);
            var newItemMenu = NewItemMenuSeeder.Seed(context, homeMenu);
            var supportMenu = SupportMenuSeeder.Seed(context, homeMenu);
            
            HelpCommandInitializer.Initialize(context, homeMenu);
            AddCommandInitializer.Initialize(context, homeMenu, newItemMenu);
            AllItemsCommandInitializer.Initialize(context, homeMenu);
            ShopsCommandInitializer.Initialize(context, homeMenu);
            WebsiteCommandInitializer.Initialize(context, homeMenu);
            WriteToSupportCommand.Initialize(context, homeMenu, supportMenu);

            BackCommandInitializer.Initialize(context);
        }
    }
}