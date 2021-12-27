using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.Common 
{
    public class BackCommandInitializer
    {
        public static Command Initialize()
        {
            return new Command(CommandKey.Back, ResourceKey.Command_Back, null);
        }
    }
}