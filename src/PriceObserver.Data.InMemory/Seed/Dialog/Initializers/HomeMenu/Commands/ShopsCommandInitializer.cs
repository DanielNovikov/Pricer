using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands;

public class ShopsCommandInitializer
{
    public static Command Initialize(Menu menu)
    {
        var command = new Command(CommandKey.Shops, ResourceKey.Command_Shops, null);
            
        menu.Commands.Add(command);

        return command;
    }
}