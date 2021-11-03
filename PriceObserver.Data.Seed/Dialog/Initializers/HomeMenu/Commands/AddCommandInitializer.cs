using PriceObserver.Data.Seed.Dialog.Initializers.Common;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Seed.Dialog.Initializers.HomeMenu.Commands
{
    public class AddCommandInitializer
    {
        public static Command Initialize(ApplicationDbContext context, Menu menu, Menu menuToRedirect)
        {
            var addCommand = CommandInitializer.Initialize(
                context,
                CommandType.Add,
                "Добавить ➕",
                menuToRedirect);

            MenuCommandInitializer.Initialize(context, menu, addCommand);

            return addCommand;
        }
    }
}