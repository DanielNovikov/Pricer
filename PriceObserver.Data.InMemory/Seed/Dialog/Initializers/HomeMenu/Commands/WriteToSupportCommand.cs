using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands;

public class WriteToSupportCommand
{
    public static Command Initialize(Menu menu, Menu menuToRedirect)
    {
        var command = new Command(CommandKey.WriteToSupport, ResourceKey.Command_WriteToSupport, menuToRedirect);

        menu.Commands.Add(command);

        return command;
    }
}