using PriceObserver.Data.Seed.Dialog.Initializers.Common;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Seed.Dialog.Initializers.NewItemMenu.Commands
{
    public class BackToHomeCommandInitializer
    {
        public static Command Initialize(ApplicationDbContext context, Menu menu, Menu menuToRedirect)
        {
            var backToHomeCommand = CommandInitializer.Initialize(
                context,
                CommandType.BackToHome,
                "Назад ◀",
                menuToRedirect);

            MenuCommandInitializer.Initialize(context, menu, backToHomeCommand);

            return backToHomeCommand;
        }
    }
}