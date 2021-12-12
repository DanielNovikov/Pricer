using System.Collections.Generic;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Data.Models
{
    public class Menu
    {
        public int Id { get; set; }
        
        public ResourceKey ResourceKey { get; set; }
        
        public MenuType Type { get; set; }
        
        public bool CanExpectInput { get; set; }
        
        public bool IsDefault { get; set; }
        
        public int? ParentId { get; set; }
        public Menu Parent { get; set; }
        
        public IList<Menu> Children { get; set; }
        
        public IList<MenuCommand> MenuCommands { get; set; }
    }
}