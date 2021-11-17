using System;

namespace PriceObserver.Model.Data
{
    public class UserToken
    {
        public int Id { get; set; }
        
        public Guid Token { get; set; }
        
        public bool Expired { get; set; }
        
        public long UserId { get; set; }
        public User User { get; set; }
    }
}