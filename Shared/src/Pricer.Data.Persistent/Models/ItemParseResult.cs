using System;
using Pricer.Data.Persistent.Models.Abstract;

namespace Pricer.Data.Persistent.Models;

public class ItemParseResult : IAggregateRoot
{
    public int Id { get; set; }
    
    public int ItemId { get; set; }
    
    public bool IsSuccess { get; set; }
    
    public DateTime Created { get; set; }
}