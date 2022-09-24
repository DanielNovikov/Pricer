using System;
using PriceObserver.Data.Persistent.Models.Abstract;

namespace PriceObserver.Data.Persistent.Models;

public class ItemPriceChange : IAggregateRoot
{
    public int Id { get; set; }
        
    public DateTime Created { get; set; }
        
    public int OldPrice { get; set; }
        
    public int NewPrice { get; set; }
        
    public int ItemId { get; set; }
}