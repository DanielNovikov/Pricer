using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models.Abstract;

namespace Pricer.Data.Persistent.Models;

public class AppNotification : IAggregateRoot
{
    public int Id { get; set; }
    
    public ResourceKey Content { get; set; }
        
    public string VideoUrl { get; set; }
    
    public bool Executed { get; set; }
}