using System.Collections.Generic;

namespace PriceObserver.Model.Data
{
    public class User
    {
        public long Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Username { get; set; }
        
        public List<Item> Items { get; set; }
    }
}