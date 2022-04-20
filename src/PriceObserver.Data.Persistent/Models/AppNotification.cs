using PriceObserver.Data.Persistent.Models.Abstract;

namespace PriceObserver.Data.Persistent.Models;

public class AppNotification : IAggregateRoot
{
    public int Id { get; set; }
        
    public string Text { get; set; }
        
    public bool Executed { get; set; }
}