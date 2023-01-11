using System;
using Pricer.Data.Persistent.Models.Abstract;

namespace Pricer.Data.Persistent.Models;

public class UserToken : IAggregateRoot
{
    public int Id { get; set; }
        
    public Guid Token { get; set; }
        
    public DateTime Expiration { get; set; }
        
    public int UserId { get; set; }
    public User User { get; set; }
}