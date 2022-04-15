using System;

namespace PriceObserver.Data.Models;

public class UserToken
{
    public int Id { get; set; }
        
    public Guid Token { get; set; }
        
    public bool Expired { get; set; }
        
    public long UserId { get; set; }
    public User User { get; set; }
}