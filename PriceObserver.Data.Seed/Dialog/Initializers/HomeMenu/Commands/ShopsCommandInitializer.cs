using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers.Common;

namespace PriceObserver.Data.Seed.Dialog.Initializers.HomeMenu.Commands
{
    public class ShopsCommandInitializer
    {
        public static Command Initialize(ApplicationDbContext context, Menu menu)
        {
            var shopsCommand = CommandInitializer.Initialize(
                context,
                CommandType.Shops,
                "Магазины 🛒");

            MenuCommandInitializer.Initialize(context, menu, shopsCommand);

            return shopsCommand;
        }
    }
}