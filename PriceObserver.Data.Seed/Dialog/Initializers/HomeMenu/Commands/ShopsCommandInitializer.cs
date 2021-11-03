using PriceObserver.Data.Seed.Dialog.Initializers.Common;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

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