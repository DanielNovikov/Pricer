using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.SettingsMenu.Commands;

public class ChangeMinimumDiscountCommandInitializer
{
    public static Command Initialize(Menu menu)
    {
        var command = new Command(
            CommandKey.ChangeMinimumDiscount,
            ResourceKey.Command_ChangeMinimumDiscount, 
            null);

        menu.Commands.Add(command);

        return command;
    }
}