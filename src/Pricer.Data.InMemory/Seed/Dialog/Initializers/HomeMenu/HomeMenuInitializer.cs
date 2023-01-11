using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.HomeMenu;

public class HomeMenuInitializer
{
    public static Menu Initialize()
    {
        return new Menu(MenuKey.Home, ResourceKey.Menu_Home, true, false, null);
    }
}