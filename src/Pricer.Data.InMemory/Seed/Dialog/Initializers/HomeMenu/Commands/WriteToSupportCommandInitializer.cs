using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands;

public class WriteToSupportCommandInitializer
{
    public static Command Initialize(Menu menu, Menu menuToRedirect)
    {
        var command = new Command(CommandKey.WriteToSupport, ResourceKey.Command_WriteToSupport, menuToRedirect);

        menu.Commands.Add(command);

        return command;
    }
}