﻿using System;
using System.Collections.Generic;

namespace PriceObserver.Data.Models
{
    public class Item
    {
        public int Id { get; set; }

        public Uri Url { get; set; }
        
        public int Price { get; set; }
        
        public string Title { get; set; }
        
        public Uri ImageUrl { get; set; }
        
        public long UserId { get; set; }
        public User User { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        
        public IList<ItemPriceChange> PriceChanges { get; set; } 
    }
}