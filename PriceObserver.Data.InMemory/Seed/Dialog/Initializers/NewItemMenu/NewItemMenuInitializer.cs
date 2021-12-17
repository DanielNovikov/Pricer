using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.NewItemMenu
{
    public class NewItemMenuInitializer
    {
        public static Menu Initialize(Menu parent)
        {
            return new Menu(
                MenuKey.NewItem,
                ResourceKey.Menu_NewItem,
                true, 
                false,
                parent);
        }
    }
}