using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models.Abstract;

namespace PriceObserver.Data.Persistent.Models;

public class AppNotification : IAggregateRoot
{
    public int Id { get; set; }
    
    public ResourceKey Content { get; set; }
        
    public string VideoUrl { get; set; }
    
    public bool Executed { get; set; }
}