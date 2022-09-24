using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.SupportMenu;

public class SupportMenuInitializer
{
    public static Menu Initialize(Menu parent)
    {
        return new Menu(
            MenuKey.Support,
            ResourceKey.Menu_Support,
            true,
            false,
            parent);
    }
}