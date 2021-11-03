using PriceObserver.Data.Seed.Dialog.Initializers.Common;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Seed.Dialog.Initializers.HomeMenu.Commands
{
    public class WebsiteCommandInitializer
    {
        public static Command Initialize(ApplicationDbContext context, Menu menu)
        {
            var websiteCommand = CommandInitializer.Initialize(
                context,
                CommandType.Website,
                "Сайт 🌍");

            MenuCommandInitializer.Initialize(context, menu, websiteCommand);

            return websiteCommand;
        }
    }
}