using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Data.Seed.Dialog.Initializers
{
    public class BackCommandInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            CommandInitializer.Initialize(
                context,
                CommandType.Back,
                ResourceKey.Command_Back);
        }
    }
}