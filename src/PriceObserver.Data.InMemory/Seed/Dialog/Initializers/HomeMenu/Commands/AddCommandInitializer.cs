using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands;

public class AddCommandInitializer
{
    public static Command Initialize(Menu menu)
    {
        var command = new Command(CommandKey.Add, ResourceKey.Command_Add, null);

        menu.Commands.Add(command);

        return command;
    }
}