using System;
using PriceObserver.Data.Persistent.Models.Abstract;

namespace PriceObserver.Data.Persistent.Models;

public class ItemParseResult : IAggregateRoot
{
    public int Id { get; set; }
    
    public int ItemId { get; set; }
    
    public bool IsSuccess { get; set; }
    
    public DateTime Created { get; set; }
}