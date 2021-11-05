using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Seed.Dialog.Initializers.Common
{
    public class BackCommandInitializer
    {
        public static Command Initialize(ApplicationDbContext context)
        {
            return CommandInitializer.Initialize(
                context,
                CommandType.Back,
                "Назад ◀");
        }
    }
}