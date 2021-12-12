using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers;

namespace PriceObserver.Data.Seed.Dialog.Seeders.HomeMenu.Commands
{
    public class WriteToSupportCommand
    {
        public static Command Initialize(ApplicationDbContext context, Menu menu, Menu menuToRedirect)
        {
            var writeToSupportCommand = CommandInitializer.Initialize(
                context,
                CommandType.WriteToSupport,
                ResourceKey.Command_WriteToSupport,
                menuToRedirect);

            MenuCommandInitializer.Initialize(context, menu, writeToSupportCommand);

            return writeToSupportCommand;
        }
    }
}