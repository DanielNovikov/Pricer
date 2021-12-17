using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Models
{
    public class Menu
    {
        public Menu( 
            MenuKey key,
            ResourceKey resourceKey,
            bool canExpectInput, 
            bool isDefault,
            Menu parent)
        {
            Key = key;
            ResourceKey = resourceKey;
            CanExpectInput = canExpectInput;
            IsDefault = isDefault;
            Parent = parent;
            Commands = new List<Command>();
        }
        
        public MenuKey Key { get; }
        
        public ResourceKey ResourceKey { get; }
        
        public bool CanExpectInput { get; }
        
        public bool IsDefault { get; }
        
        public Menu Parent { get; }
        
        public IList<Command> Commands { get; }
    }
}