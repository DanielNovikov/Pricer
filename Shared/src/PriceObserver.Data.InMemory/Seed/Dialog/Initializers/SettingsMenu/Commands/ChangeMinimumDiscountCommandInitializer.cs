using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.SettingsMenu.Commands;

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