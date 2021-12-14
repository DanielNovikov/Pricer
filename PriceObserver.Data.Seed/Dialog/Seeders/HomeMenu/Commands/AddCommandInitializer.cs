using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers;

namespace PriceObserver.Data.Seed.Dialog.Seeders.HomeMenu.Commands
{
    public class AddCommandInitializer
    {
        public static void Initialize(ApplicationDbContext context, Menu menu, Menu menuToRedirect)
        {
            var addCommand = CommandInitializer.Initialize(
                context,
                CommandType.Add,
                ResourceKey.Command_Add,
                menuToRedirect);

            MenuCommandInitializer.Initialize(context, menu, addCommand);
        }
    }
}