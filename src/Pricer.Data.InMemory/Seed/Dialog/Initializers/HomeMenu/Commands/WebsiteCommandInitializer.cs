using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands;

public class WebsiteCommandInitializer
{
    public static Command Initialize(Menu menu)
    {
        var command = new Command(CommandKey.Website, ResourceKey.Command_Website, null);
            
        menu.Commands.Add(command);

        return command;
    }
}