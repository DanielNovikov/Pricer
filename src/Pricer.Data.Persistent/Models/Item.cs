using System;
using System.Collections.Generic;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models.Abstract;

namespace Pricer.Data.Persistent.Models;

public class Item : IAggregateRoot
{
    public int Id { get; set; }

    public Uri Url { get; set; }
        
    public int Price { get; set; }
        
    public string Title { get; set; }
        
    public Uri ImageUrl { get; set; }
        
    public ShopKey ShopKey { get; set; }
        
    public bool IsAvailable { get; set; }
    
    public CurrencyKey CurrencyKey { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public IList<ItemPriceChange> PriceChanges { get; set; } = default!;
    
    public IList<ItemParseResult> ParseErrors { get; set; } = default!;
}