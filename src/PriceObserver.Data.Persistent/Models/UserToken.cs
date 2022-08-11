using System;
using PriceObserver.Data.Persistent.Models.Abstract;

namespace PriceObserver.Data.Persistent.Models;

public class UserToken : IAggregateRoot
{
    public int Id { get; set; }
        
    public Guid Token { get; set; }
        
    public DateTime Expiration { get; set; }
        
    public int UserId { get; set; }
    public User User { get; set; }
}