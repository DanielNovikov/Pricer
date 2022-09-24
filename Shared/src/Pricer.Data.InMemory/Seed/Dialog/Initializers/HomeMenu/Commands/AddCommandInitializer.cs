using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands;

public class AddCommandInitializer
{
    public static Command Initialize(Menu menu)
    {
        var command = new Command(CommandKey.Add, ResourceKey.Command_Add, null);

        menu.Commands.Add(command);

        return command;
    }
}