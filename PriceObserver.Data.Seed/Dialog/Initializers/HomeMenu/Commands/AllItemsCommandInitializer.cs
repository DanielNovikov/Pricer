using PriceObserver.Data.Seed.Dialog.Initializers.Common;
using PriceObserver.Model.Data;
using CommandType = PriceObserver.Model.Data.Enums.CommandType;

namespace PriceObserver.Data.Seed.Dialog.Initializers.HomeMenu.Commands
{
    public class AllItemsCommandInitializer
    {
        public static Command Initialize(ApplicationDbContext context, Menu menu)
        {
            var allItemsCommand = CommandInitializer.Initialize(
                context,
                CommandType.AllItems,
                "Все товары ℹ");

            MenuCommandInitializer.Initialize(context, menu, allItemsCommand);

            return allItemsCommand;
        }
    }
}