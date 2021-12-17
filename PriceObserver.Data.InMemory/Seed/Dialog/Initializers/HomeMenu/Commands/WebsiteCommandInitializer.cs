using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands
{
    public class WebsiteCommandInitializer
    {
        public static Command Initialize(Menu menu)
        {
            var command = new Command(CommandKey.Website, ResourceKey.Command_Website, menu, null);
            
            menu.Commands.Add(command);

            return command;
        }
    }
}