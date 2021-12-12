using System.Collections.Generic;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Data.Models
{
    public class Command
    {
        public int Id { get; set; }
        
        public CommandType Type { get; set; }
        
        public int? MenuToRedirectId { get; set; }
        public Menu MenuToRedirect { get; set; }
        
        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
        
        public IList<MenuCommand> CommandMenus { get; set; }
    }
}