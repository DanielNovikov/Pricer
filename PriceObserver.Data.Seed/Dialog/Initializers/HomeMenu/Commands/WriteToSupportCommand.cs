using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers.Common;

namespace PriceObserver.Data.Seed.Dialog.Initializers.HomeMenu.Commands
{
    public class WriteToSupportCommand
    {
        public static Command Initialize(ApplicationDbContext context, Menu menu, Menu menuToRedirect)
        {
            var writeToSupportCommand = CommandInitializer.Initialize(
                context,
                CommandType.WriteToSupport,
                "Поддержка 👨🏻‍💻",
                menuToRedirect);

            MenuCommandInitializer.Initialize(context, menu, writeToSupportCommand);

            return writeToSupportCommand;
        }
    }
}