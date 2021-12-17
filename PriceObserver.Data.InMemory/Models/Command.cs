using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Models
{
    public class Command
    {
        public Command(
            CommandKey key, 
            ResourceKey resourceKey,
            Menu menu,
            Menu menuToRedirect)
        {
            Key = key;
            ResourceKey = resourceKey;
            Menu = menu;
            MenuToRedirect = menuToRedirect;
        }

        public CommandKey Key { get; }
        
        public ResourceKey ResourceKey { get; }
        
        public Menu Menu { get; }
        
        public Menu MenuToRedirect { get; }
    }
}