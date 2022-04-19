using System;
using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.Persistent.Models;

public class Item
{
    public int Id { get; set; }

    public Uri Url { get; set; }
        
    public int Price { get; set; }
        
    public string Title { get; set; }
        
    public Uri ImageUrl { get; set; }
        
    public ShopKey ShopKey { get; set; }
        
    public long UserId { get; set; }
    public User User { get; set; }
        
    public IList<ItemPriceChange> PriceChanges { get; set; }
    
    public IList<ItemParseResult> ParseErrors { get; set; } 
}