using System;
using Pricer.Data.Persistent.Models.Abstract;

namespace Pricer.Data.Persistent.Models;

public class ItemPriceChange : IAggregateRoot
{
    public int Id { get; set; }
        
    public DateTime Created { get; set; }
        
    public int OldPrice { get; set; }
        
    public int NewPrice { get; set; }
        
    public int ItemId { get; set; }
}