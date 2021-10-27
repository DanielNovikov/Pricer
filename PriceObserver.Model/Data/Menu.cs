using System.Collections.Generic;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Model.Data
{
    public class Menu
    {
        public int Id { get; set; }
        
        public MenuType Type { get; set; }
        
        public bool CanExpectInput { get; set; }
        
        public bool IsDefault { get; set; }
        
        public IList<MenuCommand> MenuCommands { get; set; }
    }
}