using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.Common;

public class BackCommandInitializer
{
    public static Command Initialize()
    {
        return new Command(CommandKey.Back, ResourceKey.Command_Back, null);
    }
}