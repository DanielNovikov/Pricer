using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers;

namespace PriceObserver.Data.Seed.Dialog.Seeders.HomeMenu.Commands
{
    public class HelpCommandInitializer
    {
        public static void Initialize(ApplicationDbContext context, Menu menu)
        {
            var helpCommand = CommandInitializer.Initialize(
                context,
                CommandType.Help,
                ResourceKey.Command_Help);

            MenuCommandInitializer.Initialize(context, menu, helpCommand);
        }
    }
}