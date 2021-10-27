using System.Collections.Generic;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Model.Data
{
    public class Command
    {
        public int Id { get; set; }
        
        public CommandType Type { get; set; }
        
        public string Title { get; set; }
        
        public int? MenuToRedirectId { get; set; }
        public Menu MenuToRedirect { get; set; }
        
        public IList<MenuCommand> CommandMenus { get; set; }
    }
}