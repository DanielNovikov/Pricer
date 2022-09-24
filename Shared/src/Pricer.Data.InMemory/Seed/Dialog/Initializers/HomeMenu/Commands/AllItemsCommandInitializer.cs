using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands;

public class AllItemsCommandInitializer
{
    public static Command Initialize(Menu menu)
    {
        var command = new Command(CommandKey.AllItems, ResourceKey.Command_AllItems, null);

        menu.Commands.Add(command);

        return command;
    }
}