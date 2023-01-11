using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands;

public class HelpCommandInitializer
{
    public static Command Initialize(Menu menu)
    {
        var command = new Command(CommandKey.Help, ResourceKey.Command_Help, null);
            
        menu.Commands.Add(command);

        return command;
    }
}