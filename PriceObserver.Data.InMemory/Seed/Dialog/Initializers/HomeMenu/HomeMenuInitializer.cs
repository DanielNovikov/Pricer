using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.HomeMenu;

public class HomeMenuInitializer
{
    public static Menu Initialize()
    {
        return new Menu(MenuKey.Home, ResourceKey.Menu_Home, false, true, null);
    }
}