using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands
{
    public class AllItemsCommandInitializer
    {
        public static Command Initialize(Menu menu)
        {
            var command = new Command(CommandKey.AllItems, ResourceKey.Command_AllItems, menu, null);

            menu.Commands.Add(command);

            return command;
        }
    }
}