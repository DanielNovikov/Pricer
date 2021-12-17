using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.Models
{
    public class User
    {
        public long Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Username { get; set; }
        
        public bool IsActive { get; set; }
        
        public IList<Item> Items { get; set; }
        
        public MenuKey MenuKey { get; set; }
        
        public IList<UserToken> Tokens { get; set; }
    }
}