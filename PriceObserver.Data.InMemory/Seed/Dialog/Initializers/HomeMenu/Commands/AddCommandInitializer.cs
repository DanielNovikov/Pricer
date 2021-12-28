using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands;

public class AddCommandInitializer
{
    public static Command Initialize(Menu menu, Menu menuToRedirect)
    {
        var command = new Command(CommandKey.Add, ResourceKey.Command_Add, menuToRedirect);

        menu.Commands.Add(command);

        return command;
    }
}